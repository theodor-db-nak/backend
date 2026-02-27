using Domain.Common.Base;
using Domain.Models.ValueObjects;

namespace Domain.Models.Profiles.Repositories;

public interface IProfileRepository : IRepositoryBase<ProfileModel, Guid>
{
    Task<ProfileModel?> GetByEmailAsync(Email email, CancellationToken ct);
    Task<IEnumerable<ProfileModel>> SearchByNameRawSqlAsync(string searchTerm, CancellationToken ct);
}
