namespace Ordering.Domain.Entities;

public sealed class Customer : EntityBase<CustomerId>
{
    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public static Customer Create(CustomerId id, string name, string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        return new Customer
        {
            Id = id,
            Name = name,
            Email = email
        };
    }
}
