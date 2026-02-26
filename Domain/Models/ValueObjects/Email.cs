using Domain.Common.Exceptions;
using System.Text.RegularExpressions;

namespace Domain.Models.ValueObjects;

public partial record Email
{
    public string Value { get; init; }
    private Email(string value) => Value = value;
    
    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex EmailRegex();
    
    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainValidationException("Email cannot be empty.");

        var trimmedEmail = value.Trim().ToLower();

        if (!EmailRegex().IsMatch(trimmedEmail))
            throw new DomainValidationException("Invalid email format.");

        return new Email(trimmedEmail);
    }
    public override string ToString() => Value;
}
