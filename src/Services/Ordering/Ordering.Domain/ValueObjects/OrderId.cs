namespace Ordering.Domain.ValueObjects;

public record OrderId
{
    public Guid Value { get; }
    private OrderId(Guid value) => Value = value;

    public static OrderId Of(Guid value)
    {
        DomainException.ThrowIfNullOrWhiteSpace(value, $"{nameof(OrderId)} cannot be empty");
        return new(value);
    }
}