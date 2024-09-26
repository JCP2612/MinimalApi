using Microsoft.EntityFrameworkCore;
using ProductsNet.Source.Domain;

namespace ProductsNet.Source.Infraestructure.Data
{
    public class ApplicationDBContext(
        DbContextOptions<ApplicationDBContext> options
    ) : DbContext(options)
    {
        //Entidades

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Favorites> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
            .HasMany(p => p.Categories)
            .WithMany(c => c.Products)
            .UsingEntity<Dictionary<string, object>>(
                "CategoryProduct",
                j => j.HasOne<Category>().WithMany().HasForeignKey("CategoriesId"),
                j => j.HasOne<Product>().WithMany().HasForeignKey("ProductsId")
            );
        }
    }
}