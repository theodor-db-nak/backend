namespace Infrastructure.Persistence.Models.Entities;

public sealed class ProfileRoleEntity
{
    public required Guid RoleId { get; set; }
    public required Guid ProfileId { get; set; }
    public  RoleEntity Role { get; set; } = null!;
    public  ProfileEntity Profile { get; set; } = null!;
}
