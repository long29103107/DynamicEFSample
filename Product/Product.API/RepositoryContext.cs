using Microsoft.EntityFrameworkCore;
using Model = Product.Model;
using Product.Model;

namespace Product.Api;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
             new Category { CategoryId = 1, Name = "Electronics" },
             new Category { CategoryId = 2, Name = "Clothing" }
         );

        modelBuilder.Entity<Model.Product>().HasData(
            new Model.Product { ProductId = 1, Name = "Laptop", Price = 1000m, CategoryId = 1 },
            new Model.Product { ProductId = 2, Name = "T-Shirt", Price = 20m, CategoryId = 2 },
            new Model.Product { ProductId = 3, Name = "Smartphone", Price = 700m, CategoryId = 1 },
            new Model.Product { ProductId = 4, Name = "Headphones", Price = 100m, CategoryId = 1 },
            new Model.Product { ProductId = 5, Name = "Camera", Price = 500m, CategoryId = 1 },
            new Model.Product { ProductId = 6, Name = "Watch", Price = 150m, CategoryId = 2 },
            new Model.Product { ProductId = 7, Name = "Shoes", Price = 80m, CategoryId = 2 },
            new Model.Product { ProductId = 8, Name = "Jacket", Price = 120m, CategoryId = 2 },
            new Model.Product { ProductId = 9, Name = "Tablet", Price = 300m, CategoryId = 1 },
            new Model.Product { ProductId = 10, Name = "Mouse", Price = 50m, CategoryId = 1 }
        );

        modelBuilder.Entity<ProductDetail>().HasData(
            new ProductDetail { ProductDetailId = 1, Description = "High-performance laptop", ProductId = 1 },
            new ProductDetail { ProductDetailId = 2, Description = "Comfortable cotton t-shirt", ProductId = 2 },
            new ProductDetail { ProductDetailId = 3, Description = "Latest smartphone with advanced features", ProductId = 3 },
            new ProductDetail { ProductDetailId = 4, Description = "Noise-canceling headphones", ProductId = 4 },
            new ProductDetail { ProductDetailId = 5, Description = "High-resolution digital camera", ProductId = 5 },
            new ProductDetail { ProductDetailId = 6, Description = "Stylish analog watch", ProductId = 6 },
            new ProductDetail { ProductDetailId = 7, Description = "Durable running shoes", ProductId = 7 },
            new ProductDetail { ProductDetailId = 8, Description = "Warm winter jacket", ProductId = 8 },
            new ProductDetail { ProductDetailId = 9, Description = "Compact tablet for on-the-go use", ProductId = 9 },
            new ProductDetail { ProductDetailId = 10, Description = "Ergonomic wireless mouse", ProductId = 10 }
        );
    }

    public DbSet<Model.Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductDetail> ProductDetails { get; set; }
}
