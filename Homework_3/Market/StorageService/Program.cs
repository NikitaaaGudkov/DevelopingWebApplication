//Добавьте отдельный сервис позволяющий хранить информацию о товарах на складе/магазине. Реализуйте к нему доступ посредством API и GraphQL.
//Реализуйте API-Gateway для API сервиса склада и API-сервиса из второй лекции.

using Autofac;
using Autofac.Extensions.DependencyInjection;
using StorageService.Db;
using StorageService.Dto;
using StorageService.Mutation;
using StorageService.Query;
using StorageService.Repo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var config = new ConfigurationBuilder();
config.AddJsonFile("appsettings.json");
var cfg = config.Build();

builder.Configuration.GetConnectionString("db");

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(cb =>
{
    cb.RegisterType<ProductRepository>().As<IProductRepository>();
    cb.RegisterType<StorageRepository>().As<IStorageRepository>();
    cb.Register(c => new AppDbContext(cfg.GetConnectionString("db"))).InstancePerDependency();
});

builder.Services
    .AddGraphQLServer()
    .AddQueryType<MySimpleQuery>().
    AddMutationType<MySimpleMutation>(); ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGraphQL();

app.Run();
