using Microsoft.EntityFrameworkCore;

namespace Example1.Models
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Category> Categories { get; set; }


        private string _connectionString;

        public ProductContext()
        {

        }

        public ProductContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");

                entity.HasKey(x => x.Id).HasName("ProductID");
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(e => e.Name)
                .HasColumnName("ProductName")
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(e => e.Description)
                .HasColumnName("Description")
                .HasMaxLength(255);

                entity.Property(e => e.Cost)
                .HasColumnName("Price")
                .IsRequired();

                entity.HasOne(x => x.Category).WithMany(c => c.Products).HasForeignKey(x => x.CategoryId).HasConstraintName("CategoryToProduct").OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");

                entity.HasKey(x => x.Id).HasName("GroupID");
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(e => e.Name)
                .HasColumnName("CategoryName")
                .HasMaxLength(255)
                .IsRequired();

            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.ToTable("Storages");

                entity.HasKey(x => x.Id).HasName("StoreID");

                entity.Property(e => e.Name)
                .HasColumnName("StorageName");

                entity.HasMany(x => x.Products).WithMany(m => m.Storages).UsingEntity(j => j.ToTable("StorageProduct"));
            });

        }
    }
}
