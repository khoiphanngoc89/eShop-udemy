namespace Ordering.Domain.ValueObjects;

public record OrderName
{
    private const int MaxLength = 10;
    public string Value { get; }
    private OrderName(string value) => Value = value;

    public static OrderName Of(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Length, MaxLength);
        return new(value);
    }
}
