namespace Ordering.Domain.ValueObjects;

public record CustomerId
{
    public Guid Value { get; }
    private CustomerId(Guid value) => Value = value;

    public static CustomerId Of(Guid value)
    {
        DomainException.ThrowIfNullOrWhiteSpace(value, $"{nameof(CustomerId)} cannot be empty");
        return new(value);
    }
}
