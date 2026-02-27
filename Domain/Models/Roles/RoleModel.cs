using Domain.Common.Exceptions;
using Domain.Common.Validation;
using Domain.Models.Permissions;

namespace Domain.Models.Roles;

public sealed class RoleModel
{
    private readonly List<PermissionModel> _permissions = [];
    public RoleModel(Guid id, string name ,string? description)
    {
        Guard.AgainstEmptyGuid(id, "Id cannot be empty.");

        Guard.AgainstInvalidStr(name, 100, "First name");

        Id = id;
        Name = name.Trim();
        Description = description?.Trim();
    }
    public Guid Id { get; private init; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public IReadOnlyCollection<PermissionModel> Permissions => _permissions.AsReadOnly();

    public void Update(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainValidationException("Role name is required.");

        var trimmedName = name.Trim();
        var trimmedDescription = description?.Trim();

        if (Name == trimmedName && Description == trimmedDescription)
            return;

        Name = trimmedName;
        Description = trimmedDescription;
    }

    public void AddPermission(PermissionModel permission)
    {
        ArgumentNullException.ThrowIfNull(permission);

        if (_permissions.Any(p => p.Id == permission.Id))
            return;

        _permissions.Add(permission);
    }

    public void RemovePermission(Guid permissionId)
    {
        var permission = _permissions.FirstOrDefault(p => p.Id == permissionId);
        
        if (permission == null) 
            return;

        _permissions.Remove(permission);

    }
    public void ClearPermissions()
    {
        _permissions.Clear();
    }
}
