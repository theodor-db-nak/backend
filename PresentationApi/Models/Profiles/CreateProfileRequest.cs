
namespace PresentationApi.Models.Profiles;

public class CreateProfileRequest
{
    public required string FirstName { get; init; } = null!;
    public required string LastName { get; init; } = null!;
    public required string Email { get; init; } = null!;
    public required string Password { get; init; } = null!;
    public string? PhoneNumber { get; init; }
    public required string Street { get; init; } = null!;
    public required string City { get; init; } = null!;
    public required string PostalCode { get; init; } = null!;
    public required string Country { get; init; } = null!;
}
