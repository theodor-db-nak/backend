using Domain.Common.Exceptions;
using Domain.Common.Validation;

namespace Domain.Models.Permissions;

public sealed class PermissionModel
{
    public PermissionModel(Guid id, string name, string? description)
    {
        Guard.AgainstEmptyGuid(id, "Id cannot be empty.");

        Guard.AgainstInvalidStr(name, 100, "Name");

        Id = id;
        Name = name.Trim();
        Description = description?.Trim();
    }
    public Guid Id { get; private init; }
    public string Name { get; private set; }
    public string? Description { get; private set; }

    public void Update(string name, string? description)
    {
        Guard.AgainstInvalidStr(name, 100, "Name");

        Guard.MaxLengthValidation(description, 100, "Description");

                var trimmedName = name.Trim();
        var trimmedDescription = description?.Trim();

        if (Name == trimmedName && Description == trimmedDescription)
            return;

        Name = trimmedName;
        Description = trimmedDescription;
    }
}
