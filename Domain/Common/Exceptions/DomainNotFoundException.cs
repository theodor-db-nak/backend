namespace Domain.Common.Exceptions;

public sealed class DomainNotFoundException(string message) : DomainException(message)
{
}
