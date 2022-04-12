using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DummyShop.Persistence.Configurations;

public sealed class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.ToTable("order");

        builder.HasKey(x => x.ID);

        builder.Property(x => x.ID)
            .HasColumnName("order_id");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql(@"(SYSUTCDATETIME())");

        builder.Property(x => x.CustomerID)
            .HasColumnName("customer_id")
            .HasColumnType("bigint");

        builder.HasOne(x => x.Customer!)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.CustomerID);

        builder.HasMany(x => x.OrderLines)
            .WithOne(x => x.Order!)
            .HasForeignKey(x => x.OrderID);
    }
}