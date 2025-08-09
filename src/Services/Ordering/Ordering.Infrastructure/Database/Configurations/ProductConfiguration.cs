using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Database.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(product => product.Id);
        builder.Property(product => product.Id)
            .HasConversion(productId => productId.Value,
            dbId => ProductId.Of(dbId));

        builder.Property(product => product.Name).HasMaxLength(100).IsRequired();
    }
}
