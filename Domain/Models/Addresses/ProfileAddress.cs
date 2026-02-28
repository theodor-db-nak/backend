using Domain.Common.Exceptions;
using Domain.Common.Validation;
using Domain.Models.ValueObjects;

namespace Domain.Models.Addresses;

public sealed class ProfileAddress
{
    public ProfileAddress(Guid id, Guid profileId, Address address)
    {
        Guard.AgainstEmptyGuid(id, "Id cannot be empty.");
        Guard.AgainstEmptyGuid(profileId, "ProfileId cannot be empty.");

        if (address is null)
            throw new DomainValidationException("Address is required.");

        Id = id;
        Address = address;
    }
    public Guid Id { get; private init; }
    public Guid ProfileId { get; private set; }
    public Address Address { get; private set; }

    public void Update(Address newAddress)
    {
        ArgumentNullException.ThrowIfNull(newAddress);

        if (Address.Equals(newAddress)) return;

        Address = newAddress;
    }
}
