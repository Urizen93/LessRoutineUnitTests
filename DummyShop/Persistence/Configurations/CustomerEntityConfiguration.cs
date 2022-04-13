using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DummyShop.Persistence.Configurations;

public sealed class CustomerEntityConfiguration : IEntityTypeConfiguration<CustomerEntity>
{
    public void Configure(EntityTypeBuilder<CustomerEntity> builder)
    {
        builder.ToTable("customer");

        builder.HasKey(x => x.ID);

        builder.Property(x => x.ID)
            .HasColumnName("customer_id")
            .HasColumnType("bigint")
            .ValueGeneratedNever();

        builder.Property(x => x.Email)
            .HasColumnName("email")
            .HasMaxLength(254)
            .IsUnicode(false)
            .IsRequired();

        builder.HasIndex(x => x.Email)
            .IsUnique();
            
        builder.HasMany(x => x.Orders)
            .WithOne(x => x.Customer!)
            .HasForeignKey(x => x.CustomerID);
    }
}