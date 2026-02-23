using Domain.Common.Exceptions;

namespace Domain.Models.ValueObjects;

public record Address
{
    public  string Street { get; init; } = default!;
    public  string City { get; init; } = default!;
    public  string PostalCode { get; init; } = default!;
    public  string Country { get; init; } = default!;

    private Address() { }
    public static Address Create(string street, string city, string postalCode, string country)
    {
        if (string.IsNullOrWhiteSpace(street)) throw new DomainValidationException("Street is required");
        if (string.IsNullOrWhiteSpace(city)) throw new DomainValidationException("City is required");
        if (string.IsNullOrWhiteSpace(postalCode)) throw new DomainValidationException("Postal code is required");
        if (string.IsNullOrWhiteSpace(country)) throw new DomainValidationException("Country is required");

        return new Address
        {
            Street = street.Trim(),
            City = city.Trim(),
            PostalCode = postalCode.Trim(),
            Country = country.Trim()
        };
    }
}
