using Domain.Common.Validation;

namespace Domain.Models.Classes;

public class ClassLocation
{
    public ClassLocation(Guid id, Guid? classLocationAddressId, string name, bool isOnline)
    {
        Guard.AgainstEmptyGuid(id, "Id cannot be empty.");

        Guard.AgainstInvalidStr(name, 100, "Name");

        Id = id;
        ClassLocationAddressId = classLocationAddressId;
        Name = name.Trim();
        IsOnline = isOnline;
    }
    public Guid Id { get; private init; }
    public Guid? ClassLocationAddressId { get; private set; }
    public string Name { get; private set; }
    public bool IsOnline { get; private set; }
}
