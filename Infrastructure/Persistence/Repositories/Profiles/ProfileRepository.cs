using Domain.Common.Base;
using Domain.Models.ValueObjects;
using Domain.Profiles;
using Domain.Profiles.Repositories;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Profiles;

public sealed class ProfileRepository(CourseOnlineDbContext context) : RepositoryBase<Profile, Guid, ProfileEntity, CourseOnlineDbContext>(context), IProfileRepository
{
    public async Task<Profile?> GetByEmailAsync(Email email, CancellationToken ct)
    {
        var entity = await Set
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Email == email.Value, ct);

        return entity is null ? default : ToModel(entity);
    }

    protected override ProfileEntity ToEntity(Profile model)
    {
        throw new NotImplementedException();
    }

    protected override Profile ToModel(ProfileEntity entity)
    {
        throw new NotImplementedException();
    }
}
