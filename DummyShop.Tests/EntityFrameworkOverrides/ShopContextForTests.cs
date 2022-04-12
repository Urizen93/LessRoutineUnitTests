using DummyShop.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DummyShop.Tests.EntityFrameworkOverrides;

public sealed class ShopContextForTests : ShopContext
{
    public ShopContextForTests(DbContextOptions<ShopContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region Handle DateTimeOffsets
        
        // modelBuilder.Entity<OrderEntity>()
        //     .Property(metadata => metadata.CreatedAt)
        //     .HasConversion(new DateTimeOffsetToDateTimeConverter());
        
        #endregion
        
        #region Handle Decimals
        // modelBuilder.Entity<ProductEntity>()
        //     .Property(product => product.Price)
        //     .HasConversion<double>();
        #endregion
    }
}