namespace Ordering.Domain.ValueObjects;

public record Address
{
    public string FirstName { get; } = default!;
    public string LastName { get; } = default!;
    public string AddressLine { get; } = default!;
    public string? EmailAddress { get; } = default!;
    public string City { get; } = default!;
    public string? State { get; } = default!;
    public string Country { get; } = default!;
    public string ZipCode { get; } = default!;
    // EF Required
    protected Address() { }
    private Address(string firstName,
                    string lastName,
                    string addressLine,
                    string city,
                    string country,
                    string zipCode,
                    string? emailAddress = null,
                    string? state = null)
    {
        FirstName = firstName;
        LastName = lastName;
        AddressLine = addressLine;
        EmailAddress = emailAddress;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
    }

    public static Address Of(string firstName,
                             string lastName,
                             string addressLine,
                             string city,
                             string country,
                             string zipCode,
                             string? emailAddress = null,
                             string? state = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
        ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);
        ArgumentException.ThrowIfNullOrWhiteSpace(city);
        ArgumentException.ThrowIfNullOrWhiteSpace(country);
        ArgumentException.ThrowIfNullOrWhiteSpace(zipCode);
        return new Address(firstName, lastName, addressLine, city, country, zipCode, emailAddress, state);
    }
}
