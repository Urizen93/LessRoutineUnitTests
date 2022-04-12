using DummyShop.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DummyShop.Persistence;

public class ShopContext : DbContext
{
    public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }
        
    public DbSet<CustomerEntity> Customers => Set<CustomerEntity>();

    public DbSet<ProductEntity> Products => Set<ProductEntity>();

    public DbSet<OrderEntity> Orders => Set<OrderEntity>();
        
    public DbSet<OrderLineEntity> OrderLines => Set<OrderLineEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new CustomerEntityConfiguration())
            .ApplyConfiguration(new ProductEntityConfiguration())
            .ApplyConfiguration(new OrderEntityConfiguration())
            .ApplyConfiguration(new OrderLineEntityConfiguration())
            .Seed();

        #region Hidden so far
        // modelBuilder.HasDbFunction(() => UtcNow())
        //     .HasTranslation(_ => new SqlFunctionExpression(
        //         @"SYSUTCDATETIME()", false, typeof(DateTimeOffset), null));
        #endregion
    }
}