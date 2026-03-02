namespace Infrastructure.Persistence.Models.Entities.Roles;

public sealed class RoleEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public byte[] RowVersion { get; set; } = null!;
    public ICollection<ProfileRoleEntity> ProfileRoles { get; set; } = [];
    public ICollection<RolePermissionEntity> RolePermissions { get; set; } = []; 
}
