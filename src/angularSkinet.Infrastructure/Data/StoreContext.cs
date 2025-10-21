using angularSkinet.Core.Entities;
using angularSkinet.Infrastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace angularSkinet.Infrastructure.Data;

public class StoreContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProductConfiguration());
    }
}
