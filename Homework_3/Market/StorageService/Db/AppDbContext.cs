using Microsoft.EntityFrameworkCore;

namespace StorageService.Db
{
    public class AppDbContext : DbContext
    {
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Product> Products { get; set; }

        private string _connectionString;
        public AppDbContext()
        {
            
        }

        public AppDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(_connectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("product_pkey");
                entity.ToTable("products");

                entity.HasOne(x => x.Storage).WithMany(c => c.Products).HasForeignKey(x => x.StorageId).HasConstraintName("StorageToProduct").OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("storage_pkey");
                entity.ToTable("storages");
            });

        }
    }
}
