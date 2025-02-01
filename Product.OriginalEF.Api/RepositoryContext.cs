using Microsoft.EntityFrameworkCore;
using Model = Product.Model;
using Product.Model;

namespace Product.OriginalEF.Api;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
    {
    }

    public DbSet<Model.Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductDetail> ProductDetails { get; set; }
}
