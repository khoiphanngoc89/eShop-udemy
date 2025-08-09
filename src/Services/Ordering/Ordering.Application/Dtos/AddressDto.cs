namespace Ordering.Application.Dtos;

public record AddressDto(
    string FirstName,
    string LastName,
    string EmailAddress,
    string AddressLine,
    string Street,
    string City,
    string State,
    string ZipCode,
    string Country);