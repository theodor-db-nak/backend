namespace Application.Profiles.Inputs;

public sealed record CreateProfileInput
(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string? PhoneNumber,
    string Street,
    string City,
    string PostalCode,
    string Country
);