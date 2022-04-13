using DummyShop.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;

namespace DummyShop.Persistence;

public class ShopContext : DbContext
{
    private readonly Action<ModelBuilder> _seed;

    public ShopContext(
        DbContextOptions<ShopContext> options,
        Action<ModelBuilder>? seed = null) : base(options) =>
        _seed = seed ?? (_ => { });

    public DbSet<CustomerEntity> Customers => Set<CustomerEntity>();

    public DbSet<ProductEntity> Products => Set<ProductEntity>();

    public DbSet<OrderEntity> Orders => Set<OrderEntity>();
        
    public DbSet<OrderLineEntity> OrderLines => Set<OrderLineEntity>();

    public static DateTimeOffset UtcNow() => throw new NotImplementedException();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new CustomerEntityConfiguration())
            .ApplyConfiguration(new ProductEntityConfiguration())
            .ApplyConfiguration(new OrderEntityConfiguration())
            .ApplyConfiguration(new OrderLineEntityConfiguration());

        _seed(modelBuilder);

        modelBuilder.HasDbFunction(() => UtcNow())
            .HasTranslation(_ => new SqlFunctionExpression(
                @"SYSUTCDATETIME()", false, typeof(DateTimeOffset), null));
    }
}