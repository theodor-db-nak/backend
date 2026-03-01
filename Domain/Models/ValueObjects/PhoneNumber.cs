using System.Text.RegularExpressions;
using Domain.Common.Exceptions;

namespace Domain.Models.ValueObjects;

public partial record PhoneNumber
{
    public string Value { get; init; }
    private PhoneNumber() => Value = null!;
    private PhoneNumber(string value) => Value = value;


    [GeneratedRegex(@"^\+?[0-9]\d{1,14}$")]
    private static partial Regex PhoneRegex();
    
    public static PhoneNumber Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null!;

        var cleaned = value
            .Replace(" ", "")
            .Replace("-", "")
            .Replace("(", "")
            .Replace(")", "");

        if (!PhoneRegex().IsMatch(cleaned))
            throw new DomainValidationException("Invalid phone number format.");

        return new PhoneNumber(cleaned);
    }

    public static implicit operator string(PhoneNumber phone) => phone.Value;

    public static implicit operator PhoneNumber(string value) => Create(value);
}