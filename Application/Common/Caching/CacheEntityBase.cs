using Microsoft.Extensions.Caching.Memory;

namespace Application.Common.Caching;

public abstract class CacheEntityBase<TEntity, TId>(IMemoryCache cache) : ICacheEntityBase<TEntity, TId>

{
    public Task<IReadOnlyList<TEntity>?> GetOrCreateAllAsync(Func<CancellationToken, Task<IReadOnlyList<TEntity>>> factory, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity?> GetOrCreateByIdAsync(TId id, Func<CancellationToken, Task<TEntity?>> factory, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity?> GetOrCreateByPropertyNameAsync(string cachedPropertyName, string cachedPropertyValue, Func<CancellationToken, Task<TEntity?>> factory, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public void ResetEntity(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public void SetEntity(TEntity entity)
    {
        throw new NotImplementedException();
    }
}
