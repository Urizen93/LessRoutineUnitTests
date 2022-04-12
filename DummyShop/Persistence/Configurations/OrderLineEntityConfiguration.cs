using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DummyShop.Persistence.Configurations;

public sealed class OrderLineEntityConfiguration : IEntityTypeConfiguration<OrderLineEntity>
{
    public void Configure(EntityTypeBuilder<OrderLineEntity> builder)
    {
        builder.ToTable("order_line");

        builder.HasKey(x => x.ID);

        builder.Property(x => x.ID)
            .HasColumnName("order_line_id");

        builder.Property(x => x.Quantity)
            .HasColumnName("quantity");

        builder.Property(x => x.ProductID)
            .HasColumnName("product_id");

        builder.Property(x => x.OrderID)
            .HasColumnName("order_id");

        builder.HasOne(x => x.Product!)
            .WithMany(x => x.OrderLines)
            .HasForeignKey(x => x.ProductID);
            
        builder.HasOne(x => x.Order!)
            .WithMany(x => x.OrderLines)
            .HasForeignKey(x => x.OrderID);
    }
}