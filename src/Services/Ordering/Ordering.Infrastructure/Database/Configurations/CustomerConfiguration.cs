using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Database.Configurations;

public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(customer => customer.Id);
        builder.Property(customer => customer.Id)
            .HasConversion(customerId => customerId.Value, // storing in the db
            dbId => CustomerId.Of(dbId)); // reading back to the domain object

        builder.Property(customer => customer.Name).HasMaxLength(100).IsRequired();

        builder.Property(customer => customer.Email).HasMaxLength(255);
        builder.HasIndex(customer => customer.Email).IsUnique();
    }
}
