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
        if (string.IsNullOrWhiteSpace(street)) throw new ArgumentException("Street is required");
        if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City is required");
        if (string.IsNullOrWhiteSpace(postalCode)) throw new ArgumentException("Postal code is required");
        if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException("Country is required");


        return new Address
        {
            Street = street,
            City = city,
            PostalCode = postalCode,
            Country = country
        };
    }

}
