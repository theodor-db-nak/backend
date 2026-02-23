using Domain.Common.Exceptions;
using Domain.Models.ValueObjects;

namespace Domain.Profiles;

public sealed class Profile
{
    public Profile(Guid id, string firstName, string lastName, Email email, string password, PhoneNumber? phoneNumber)
    {
        if (id == Guid.Empty)
            throw new DomainValidationException("Id cannot be empty.");

        if (string.IsNullOrWhiteSpace(firstName))
            throw new DomainValidationException("First name is required.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new DomainValidationException("Last name is required.");

        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            throw new DomainValidationException("Password must be at least 8 characters long.");

        Id = id;
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
    }

    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Email Email { get; private set; }
    public string Password { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; } 
}
