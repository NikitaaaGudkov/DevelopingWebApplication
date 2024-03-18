// Доработайте контроллер, реализовав в нем метод возврата CSV-файла с товарами.
// Доработайте контроллер, реализовав статичный файл со статистикой работы кэш. Сделайте его доступным по ссылке.
// Перенесите строку подключения для работы с базой данных в конфигурационный файл приложения.

using Autofac;
using Autofac.Extensions.DependencyInjection;
using Example1.Abstraction;
using Example1.Models;
using Example1.Repo;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());


var config = new ConfigurationBuilder();
config.AddJsonFile("appsettings.json");
var cfg = config.Build();

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterType<ProductRepository>().As<IProductRepository>();
    containerBuilder.Register(c => new ProductContext(cfg.GetConnectionString("db"))).InstancePerDependency();
});

builder.Services.AddMemoryCache(o => o.TrackStatistics = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var staticFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles");
Directory.CreateDirectory(staticFilesPath);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(staticFilesPath),
    RequestPath = "/static"
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
