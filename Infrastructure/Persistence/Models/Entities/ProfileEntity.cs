using Domain.Models.ValueObjects;
using Infrastructure.Persistence.Models.Entities.Addresses;
using Infrastructure.Persistence.Models.Entities.Classes;
using Infrastructure.Persistence.Models.Entities.Courses;
using Infrastructure.Persistence.Models.Entities.Roles;

namespace Infrastructure.Persistence.Models.Entities;

public sealed class ProfileEntity
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required Email Email { get; set; }
    public required string Password { get; set; }
    public PhoneNumber? PhoneNumber { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime ModifiedAt { get; set; }
    public byte[] RowVersion { get; set; } = null!;
    public ProfileAddressEntity? Address { get; set; }
    public ICollection<ProfileRoleEntity> ProfileRoles { get; set; } = [];
    public ICollection<ClassProfileEntity> ClassProfiles { get; set; } = [];
    public ICollection<CourseProfileEntity> CourseProfiles { get; set; } = [];
}
