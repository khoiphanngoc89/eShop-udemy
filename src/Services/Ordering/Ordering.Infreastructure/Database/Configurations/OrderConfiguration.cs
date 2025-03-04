using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Database.Configurations;

public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(order => order.Id);
        builder.Property(order => order.Id)
            .HasConversion(id => id.Value,
            value => OrderId.Of(value));

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(order => order.CustomerId)
            .IsRequired();

        builder.HasMany(builder => builder.Items)
            .WithOne()
            .HasForeignKey(item => item.OrderId);

        // complexproperty using for value object
        builder.ComplexProperty(
            order => order.OrderName, nameBuilder =>
        {
            nameBuilder.Property(name => name.Value)
                .HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.ComplexProperty(
            order => order.ShippingAddress, addressBuilder => ConfigureAddress(addressBuilder));

        builder.ComplexProperty(
            order => order.BillingAddress, addressBuilder => ConfigureAddress(addressBuilder));

        builder.ComplexProperty(
            order => order.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(payment => payment.CardHolderName)
                    .HasMaxLength(100)
                    .IsRequired();

                paymentBuilder.Property(payment => payment.CardNumber)
                    .HasMaxLength(24)
                    .IsRequired();

                paymentBuilder.Property(payment => payment.Expiration)
                    .HasMaxLength(10)
                    .IsRequired();

                paymentBuilder.Property(payment => payment.CVV)
                    .HasMaxLength(3)
                    .IsRequired();

                paymentBuilder.Property(payment => payment.PaymentMethod);
            });

        // enum conversion
        builder.Property(order => order.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(orderStatus => orderStatus.ToString(),
                dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

        // skipping total price as it's a calculated field
    }

    private void ConfigureAddress(ComplexPropertyBuilder<Address> addressBuilder)
    {
        addressBuilder.Property(address => address.FirstName)
                    .HasMaxLength(100)
                    .IsRequired();
        addressBuilder.Property(address => address.LastName)
            .HasMaxLength(100)
            .IsRequired();
        addressBuilder.Property(address => address.EmailAddress)
            .HasMaxLength(255);

        addressBuilder.Property(address => address.AddressLine)
            .HasMaxLength(180)
            .IsRequired();

        addressBuilder.Property(address => address.City)
            .HasMaxLength(40)
            .IsRequired();

        addressBuilder.Property(address => address.State)
            .HasMaxLength(50);
            
        addressBuilder.Property(address => address.Country)
            .HasMaxLength(50)
            .IsRequired();

        addressBuilder.Property(address => address.ZipCode)
            .HasMaxLength(10)
            .IsRequired();
    }
}
