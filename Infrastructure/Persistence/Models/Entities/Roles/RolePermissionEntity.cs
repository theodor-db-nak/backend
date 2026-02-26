namespace Infrastructure.Persistence.Models.Entities.Roles;

public class RolePermissionEntity
{
    public required Guid RoleId { get; set; } 
    public required Guid PermissionId { get; set; }
    public RoleEntity Role { get; set; } = null!;
    public PermissionEntity Permission {  get; set; } = null!;
}
