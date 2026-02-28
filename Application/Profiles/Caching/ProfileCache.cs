using Application.Common.Caching;

using Microsoft.Extensions.Caching.Memory;

namespace Application.Instructors.Caching;

public sealed class InstructorCache(IMemoryCache cache) : CacheEntityBase<Instructor, string>(cache), IInstructorCache
{
    protected override string GetId(Instructor entity) => entity.Id;

    protected override IEnumerable<(string PropertyName, string Value)> GetCachedProperties(Instructor entity)
    {
        if (!string.IsNullOrWhiteSpace(entity.Email))
            yield return ("email", entity.Email);
    }

    public Task<Instructor?> GetByIdAsync(string id, Func<CancellationToken, Task<Instructor?>> factory, CancellationToken ct)
        => GetOrCreateByIdAsync(id, factory, ct);

    public Task<Instructor?> GetByEmailAsync(string email, Func<CancellationToken, Task<Instructor?>> factory, CancellationToken ct)
        => GetOrCreateByPropertyNameAsync("email", email, factory, ct);

    public Task<IReadOnlyList<Instructor>?> GetAllAsync(Func<CancellationToken, Task<IReadOnlyList<Instructor>>> factory, CancellationToken ct)
        => GetOrCreateAllAsync(factory, ct);

}
