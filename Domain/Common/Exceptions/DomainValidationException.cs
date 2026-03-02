namespace Domain.Common.Exceptions;

public sealed class DomainValidationException(string message) : DomainException(message)
{
}
