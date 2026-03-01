using Domain.Models.Addresses;
using Domain.Models.Classes;
using Domain.Models.Courses;
using Domain.Models.Profiles;
using Domain.Models.Profiles.Repositories;
using Domain.Models.Roles;
using Domain.Models.ValueObjects;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Extensions;
using Infrastructure.Persistence.Models.Entities;
using Infrastructure.Persistence.Models.Entities.Addresses;
using Infrastructure.Persistence.Models.Entities.Classes;
using Infrastructure.Persistence.Models.Entities.Courses;
using Infrastructure.Persistence.Models.Entities.Roles;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Profiles;

public sealed class ProfileRepository(CourseOnlineDbContext context) : RepositoryBase<Profile, Guid, ProfileEntity, CourseOnlineDbContext>(context), IProfileRepository
{
    public override async Task<Profile?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var entity = await Set
            .Include(p => p.Address)
            .Include(p => p.ProfileRoles).ThenInclude(pr => pr.Role)
            .Include(p => p.ClassProfiles).ThenInclude(cp => cp.Class)
            .Include(p => p.CourseProfiles).ThenInclude(cp => cp.Course)
            .FirstOrDefaultAsync(p => p.Id == id, ct);

        return entity is null ? null : ToModel(entity);
    }
    public override async Task<IReadOnlyList<Profile>> GetAllAsync(CancellationToken ct = default)
    {
        var entities = await Set
        .AsNoTracking()
        .Include(p => p.Address)
        .Include(p => p.ProfileRoles).ThenInclude(pr => pr.Role)
        .Include(p => p.ClassProfiles).ThenInclude(cp => cp.Class)
        .Include(p => p.CourseProfiles).ThenInclude(cp => cp.Course)
        .ToListAsync(ct);

        return [.. entities.Select(ToModel)];
    }
    public override async Task<Profile?> UpdateAsync(Guid id, Profile model, CancellationToken ct = default)
    {
        try
        {
            var existingEntity = await Set
                .Include(p => p.Address)
                .Include(p => p.ProfileRoles)
                .Include(p => p.ClassProfiles)
                .Include(p => p.CourseProfiles)
                .FirstOrDefaultAsync(p => p.Id == id, ct);

            if (existingEntity is null) return default;

            var updatedValues = ToEntity(model);

            _context.Entry(existingEntity).CurrentValues.SetValues(updatedValues);

            if (existingEntity.Address != null && updatedValues.Address != null)
            {
                existingEntity.Address.Address = updatedValues.Address.Address;
            }

            existingEntity.ProfileRoles.Sync(updatedValues.ProfileRoles, r => r.RoleId);
            existingEntity.ClassProfiles.Sync(updatedValues.ClassProfiles, c => c.ClassId);
            existingEntity.CourseProfiles.Sync(updatedValues.CourseProfiles, c => c.CourseId);

            await _context.SaveChangesAsync(ct);

            return ToModel(existingEntity);
        }
        catch
        {
            return default;
        }
    }
    public override async Task<bool> RemoveAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            var entity = await Set.FindAsync([id], ct);

            if (entity is null) return false; 

            Set.Remove(entity);

            var result = await _context.SaveChangesAsync(ct);

            return result > 0;
        }
        catch
        {
            return false;
        }
    }
    public async Task<Profile?> GetByEmailAsync(Email email, CancellationToken ct)
    {
        var entity = await Set
                .AsNoTracking()
                .Include(p => p.Address)
                .Include(p => p.ProfileRoles).ThenInclude(pr => pr.Role)
                .Include(p => p.ClassProfiles).ThenInclude(cp => cp.Class)
                .Include(p => p.CourseProfiles).ThenInclude(cp => cp.Course)
                .FirstOrDefaultAsync(p => p.Email == email.Value, ct);

        return entity is null ? default : ToModel(entity);
    }
    public async Task<IReadOnlyList<Profile>> SearchByNameRawSqlAsync(string searchTerm, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(searchTerm)) return [];

        var sqlSearch = $"%{searchTerm}%";

        var entities = await Set
            .FromSqlInterpolated($@"
            SELECT p.* FROM Profiles p
            LEFT JOIN ProfileAddresses a ON p.Id = a.ProfileId
            LEFT JOIN ProfileRoles pr ON p.Id = pr.ProfileId
            LEFT JOIN ClassProfiles cp ON p.Id = cp.ProfileId
            LEFT JOIN CourseProfiles cup ON p.Id = cup.ProfileId
            WHERE p.FirstName LIKE {sqlSearch} 
               OR p.LastName LIKE {sqlSearch}
            ")
            .AsNoTracking()

            .Include(p => p.Address)
            .Include(p => p.ProfileRoles)
            .Include(p => p.ClassProfiles)
            .Include(p => p.CourseProfiles)
            .ToListAsync(ct);

        return [.. entities.Select(ToModel)];
    }
    public override async Task<Profile?> AddAsync(Profile model, CancellationToken ct = default)
    {
        try
        {
            var entity = ToEntity(model);

            await Set.AddAsync(entity, ct);

            await _context.SaveChangesAsync(ct);

            return ToModel(entity);
        }
        catch
        {
            return default;
        }
    }
    protected override ProfileEntity ToEntity(Profile profile)
    {
        var entity = new ProfileEntity
        {
            Id = profile.Id,
            FirstName = profile.FirstName,
            LastName = profile.LastName,
            Email = profile.Email,
            Password = profile.Password,
            PhoneNumber = profile.PhoneNumber,

            Address = new ProfileAddressEntity
            {
                Id = profile.Address.Id,
                ProfileId = profile.Id,
                Address = profile.Address.Address,
            },

            ProfileRoles = [.. profile.Roles.Select(pr => new ProfileRoleEntity
            {
                ProfileId = profile.Id,
                RoleId = pr.Id
            })],
            ClassProfiles = [.. profile.Classes.Select(cp => new ClassProfileEntity
            {
                ProfileId = profile.Id,
                ClassId = cp.Id
            })],
            CourseProfiles = [.. profile.Courses.Select(cp => new CourseProfileEntity
            {
                ProfileId = profile.Id,
                CourseId = cp.Id
            })]
        };
        return entity;
    }

    protected override Profile ToModel(ProfileEntity entity)
    {
        ProfileAddress? addressModel = null;

        if (entity.Address is not null)
        {
            var addressValues = Address.Create(
                entity.Address.Address.Street,
                entity.Address.Address.City,
                entity.Address.Address.PostalCode,
                entity.Address.Address.Country);

            addressModel = new ProfileAddress(entity.Address.Id, entity.Address.ProfileId, addressValues);
        }

        var profile = new Profile(
            entity.Id,
            entity.FirstName,
            entity.LastName,
            entity.Email,      
            entity.Password,
            entity.PhoneNumber, 
            addressModel!      
        );

        foreach (var pr in entity.ProfileRoles)
            if (pr.Role != null)
                profile.AddRole(new Role(pr.Role.Id, pr.Role.Name, pr.Role.Description));

        foreach (var cp in entity.ClassProfiles)
            if (cp.Class != null)
                profile.AddClass(new Class(cp.Class.Id, cp.Class.Name, cp.Class.Seats));

        foreach (var cp in entity.CourseProfiles)
            if (cp.Course != null)
                profile.AddCourse(new Course(cp.Course.Id, cp.Course.Name));

        return profile;
    }
}
