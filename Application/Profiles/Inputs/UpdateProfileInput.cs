using Domain.Models.Addresses;
using Domain.Models.ValueObjects;

namespace Application.Profiles.Inputs;

public sealed record UpdateProfileInput
(
    Guid Id,                
    string FirstName,
    string LastName,
    string? PhoneNumber,    
    Address? Address   
);