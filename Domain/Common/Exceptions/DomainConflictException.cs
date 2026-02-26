namespace Domain.Common.Exceptions;

public sealed class DomainConflictException(string message) : DomainException(message)
{
}