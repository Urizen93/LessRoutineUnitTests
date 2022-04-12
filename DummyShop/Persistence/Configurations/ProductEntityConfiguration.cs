using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DummyShop.Persistence.Configurations;

public sealed class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.ToTable("product");

        builder.HasKey(x => x.ID);

        builder.Property(x => x.ID)
            .HasColumnName("product_id");

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(200)
            .IsUnicode()
            .IsRequired();

        builder.Property(x => x.Price)
            .HasColumnName("price");

        builder.HasMany(x => x.OrderLines)
            .WithOne(x => x.Product!)
            .HasForeignKey(x => x.ProductID);
    }
}