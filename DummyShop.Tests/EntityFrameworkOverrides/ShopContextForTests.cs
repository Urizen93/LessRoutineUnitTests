using DummyShop.Persistence;
using DummyShop.Tests.EntityFrameworkOverrides.Converters;
using Microsoft.EntityFrameworkCore;

namespace DummyShop.Tests.EntityFrameworkOverrides;

public sealed class ShopContextForTests : ShopContext
{
    public ShopContextForTests(DbContextOptions<ShopContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<OrderEntity>()
            .Property(metadata => metadata.CreatedAt)
            .HasConversion(new DateTimeOffsetToDateTimeConverter());
        
        modelBuilder.Entity<ProductEntity>()
            .Property(product => product.Price)
            .HasConversion<double>();
    }
}