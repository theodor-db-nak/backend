using Domain.Common.Exceptions;

namespace Domain.Common.Validation;

public static class Guard
{
    public static void AgainstEmptyGuid(Guid value, string message)
    {
        if (value == Guid.Empty)
            throw new DomainValidationException(message);
    }

    public static void AgainstInvalidStr(string value, int maxLength, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainValidationException($"{fieldName} is required.");

        if (value.Length > maxLength)
            throw new DomainValidationException($"{fieldName} cannot exceed {maxLength} characters.");
    }
    public static void MaxLengthValidation(string? value, int maxLength, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            return;    
        if (value.Length > maxLength)
            throw new DomainValidationException($"{fieldName} cannot exceed {maxLength} characters.");
    }
    public static void TransferValidation(Guid newId, Guid oldId)
    {
        if (oldId == newId)
            throw new DomainValidationException("Target cannot be the same as before.");

        if (newId == Guid.Empty)
            throw new DomainValidationException("Target is required.");
    }
    public static void AgainstInsecurePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            throw new DomainValidationException("Password must be at least 8 characters long.");
    }
}
