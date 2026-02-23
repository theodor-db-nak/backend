using System.Text.RegularExpressions;
using Domain.Common.Exceptions;

namespace Domain.Models.ValueObjects;

public partial record PhoneNumber
{
    public string Value { get; init; }

    [GeneratedRegex(@"^\+?[1-9]\d{1,14}$")]
    private static partial Regex PhoneRegex();

    private PhoneNumber(string value) => Value = value;

    public static PhoneNumber Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainValidationException("Phone number cannot be empty.");

        var cleaned = value.Replace(" ", "").Replace("-", "");

        if (!PhoneRegex().IsMatch(cleaned))
            throw new DomainValidationException("Invalid phone number format.");

        return new PhoneNumber(cleaned);
    }

    public override string ToString() => Value;
}