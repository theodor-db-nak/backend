using Application.Common.Caching;
using Application.Profiles.Contracts;
using Domain.Models.Profiles;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Profiles.Caching;

public sealed class ProfileCache(IMemoryCache cache) : CacheEntityBase<Profile, Guid>(cache), IProfileCache
{
    protected override Guid GetId(Profile entity) => entity.Id;

    protected override IEnumerable<(string PropertyName, string Value)> GetCachedProperties(Profile entity)
    {
        if (!string.IsNullOrWhiteSpace(entity.Email))
            yield return ("email", entity.Email);
    }
    public Task<IReadOnlyList<Profile>> GetAllAsync(Func<CancellationToken, Task<IReadOnlyList<Profile>>> factory, CancellationToken ct)
        => GetOrCreateAllAsync(factory, ct);

    public Task<Profile?> GetByEmailAsync(string email, Func<CancellationToken, Task<Profile?>> factory, CancellationToken ct)
        => GetByPropertyAsync("email", email, factory, ct);

    public Task<Profile?> GetByIdAsync(Guid id, Func<CancellationToken, Task<Profile?>> factory, CancellationToken ct)
        => GetOrCreateByIdAsync(id, factory, ct);

    public Task<IEnumerable<Profile>> SearchByNameRawSqlAsync(string searchTerm, Func<CancellationToken, Task<IEnumerable<Profile>>> factory, CancellationToken ct)
        => GetBySearchAsync(searchTerm, factory, ct);
}
