namespace Ordering.Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException()
    {
    }

    public DomainException(string message)
        : base($"Domain Exception: \"{message}\"")
    {
    }
    public DomainException(string message, Exception innerException)
        : base($"Domain Exception: \"{message}\"", innerException)
    {
    }

    public static void ThrowIfNullOrWhiteSpace<T>(T value, string message)
    {
        if (value is null || (value is string str && string.IsNullOrWhiteSpace(str)))
        {
            throw new DomainException(message);
        }

        if (value is Guid guid && guid == Guid.Empty)
        {
            throw new DomainException(message);
        }
    }
}
