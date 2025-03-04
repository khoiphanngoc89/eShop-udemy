using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infreastructure.Database.Configurations;

public sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(orderItem => orderItem.Id);
        builder.Property(orderItem => orderItem.Id)
            .HasConversion(id => id.Value,
            value => OrderItemId.Of(value));

        // configure ProductId forgien key
        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(orderItem => orderItem.ProductId);

        builder.Property(orderItem => orderItem.Quantity)
            .IsRequired();

        builder.Property(orderItem => orderItem.Price).IsRequired();
    }
}
