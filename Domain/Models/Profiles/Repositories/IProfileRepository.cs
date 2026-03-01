using Domain.Common.Base;
using Domain.Models.ValueObjects;

namespace Domain.Models.Profiles.Repositories;

public interface IProfileRepository : IRepositoryBase<Profile, Guid>
{
    Task<Profile?> GetByEmailAsync(Email email, CancellationToken ct);
    Task<IReadOnlyList<Profile>> SearchByNameRawSqlAsync(string searchTerm, CancellationToken ct);
}
