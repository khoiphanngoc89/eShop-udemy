namespace Ordering.Domain.ValueObjects;

public record Payment
{
    private const int CVVLength = 3;
    public string CardHolderName { get; } = default!;
    public string CardNumber { get; } = default!;
    public string Expiration { get; } = default!;
    public string CVV { get; } = default!;
    public string PaymentMethod { get; } = default!;
    protected Payment() { }
    private Payment(string cardHolderName, string cardNumber, string expiration, string cvv, string paymentMethod)
    {
        CardHolderName = cardHolderName;
        CardNumber = cardNumber;
        Expiration = expiration;
        CVV = cvv;
        PaymentMethod = paymentMethod;
    }

    public static Payment Of(string cardHolderName, string cardNumber, string expiration, string cvv, string paymentMethod)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cardHolderName);
        ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(expiration);
        ArgumentException.ThrowIfNullOrWhiteSpace(cvv);
        ArgumentOutOfRangeException.ThrowIfNotEqual(cvv.Length, CVVLength);

        return new Payment(cardHolderName, cardNumber, expiration, cvv, paymentMethod);
    }
}
