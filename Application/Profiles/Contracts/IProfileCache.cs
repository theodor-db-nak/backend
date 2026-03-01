using Application.Common.Caching;
using Domain.Models.Profiles;

namespace Application.Profiles.Contracts;

public interface IProfileCache : ICacheEntityBase<Profile, Guid>
{
    Task<IReadOnlyList<Profile>> GetAllAsync(Func<CancellationToken, Task<IReadOnlyList<Profile>>> factory, CancellationToken ct);
    Task<Profile?> GetByEmailAsync(string email, Func<CancellationToken, Task<Profile?>> factory, CancellationToken ct);
    Task<Profile?> GetByIdAsync(Guid id, Func<CancellationToken, Task<Profile?>> factory, CancellationToken ct);
    Task<IEnumerable<Profile>> SearchByNameRawSqlAsync(string searchTerm, Func<CancellationToken, Task<IEnumerable<Profile>>> factory, CancellationToken ct);
}
