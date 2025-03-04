namespace Ordering.Domain.ValueObjects;

public record ProductId
{
    public Guid Value { get; }
    private ProductId(Guid value) => Value = value;

    public static ProductId Of(Guid value)
    {
        DomainException.ThrowIfNullOrWhiteSpace(value, $"{nameof(ProductId)} cannot be empty");
        return new(value);
    }
}
