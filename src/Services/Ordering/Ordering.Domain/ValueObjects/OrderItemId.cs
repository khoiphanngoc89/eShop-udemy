namespace Ordering.Domain.ValueObjects;

public record OrderItemId
{
    public Guid Value { get; }
    private OrderItemId(Guid value) => Value = value;

    public static OrderItemId Of(Guid value)
    {
        DomainException.ThrowIfNullOrWhiteSpace(value, $"{nameof(OrderItemId)} cannot be empty");
        return new(value);
    }
}
