using Application.Profiles.Inputs;
using Domain.Models.Addresses;
using Domain.Models.Profiles;
using Domain.Models.ValueObjects;

namespace Application.Profiles.Factories;

public static class ProfileFactory
{
    public static Profile Create(CreateProfileInput input)
    {
        var profileId = Guid.NewGuid();
        var email = Email.Create(input.Email);
        var phoneNumber = PhoneNumber.Create(input.PhoneNumber!);

        var address = Address.Create(
            input.Street,
            input.City,
            input.PostalCode,
            input.Country
        );

        var profileAddress = new ProfileAddress(
            Guid.NewGuid(),
            profileId,
            address
        );

        var profile = new Profile(
            profileId,
            input.FirstName,
            input.LastName,
            email,
            input.Password,
            phoneNumber,
            profileAddress
        );

        return profile;
    }
}
