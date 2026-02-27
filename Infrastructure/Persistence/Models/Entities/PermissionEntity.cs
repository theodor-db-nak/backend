using Infrastructure.Persistence.Models.Entities.Roles;

namespace Infrastructure.Persistence.Models.Entities;

public sealed class PermissionEntity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public byte[] RowVersion { get; set; } = null!;
    public ICollection<RolePermissionEntity> RolePermissions { get; set; } = [];
}
