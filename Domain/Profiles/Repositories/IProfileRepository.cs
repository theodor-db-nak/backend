

using Domain.Common.Base;
using Domain.Models.ValueObjects;

namespace Domain.Profiles.Repositories;

public interface IProfileRepository : IRepositoryBase<Profile, Guid>
{
    Task<Profile?> GetByEmailAsync(Email email, CancellationToken ct);
}
