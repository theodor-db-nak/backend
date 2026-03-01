using Application.Extensions.Caching;
using Microsoft.Extensions.Caching.Memory;


namespace Application.Common.Caching;

public abstract class CacheEntityBase<TEntity, TId>(IMemoryCache cache) : ICacheEntityBase<TEntity, TId>

{
    protected virtual string Prefix => typeof(TEntity).Name.ToLowerInvariant();
    protected abstract TId GetId(TEntity entity);
    protected virtual IEnumerable<(string PropertyName, string Value)> GetCachedProperties(TEntity entity) => [];
    protected virtual string NormalizeCachedPropertyValue(string value) => value.Trim().ToLowerInvariant();

    protected virtual MemoryCacheEntryOptions EntityOptions => new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
        SlidingExpiration = TimeSpan.FromMinutes(2),
    };

    protected virtual MemoryCacheEntryOptions ListOptions => new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30),
        SlidingExpiration = TimeSpan.FromSeconds(10),
    };

    protected string IdKey(TId id) => $"{Prefix}:id:{id}";
    protected string IndexKey(string cachedPropertyName, string cachedPropertyValue) => $"{Prefix}:{cachedPropertyName}:{cachedPropertyValue}";
    protected string AllKey => $"{Prefix}:all";

    public virtual void SetEntity(TEntity entity)
    {
        var id = GetId(entity);
        var idKey = IdKey(id);

        cache.Set(idKey, entity, EntityOptions);

        foreach (var (prop, val) in GetCachedProperties(entity))
        {
            if (string.IsNullOrWhiteSpace(val)) continue;

            var normalizedVal = NormalizeCachedPropertyValue(val);
            cache.Set(IndexKey(prop, normalizedVal), id, EntityOptions);
        }

        cache.Remove(AllKey);
    }

    public virtual Task<TEntity?> GetOrCreateByIdAsync(TId id, Func<CancellationToken, Task<TEntity?>> factory, CancellationToken ct)
        => cache.GetOrCreateAsync(
            IdKey(id),
            async (entry, token) =>
            {
                entry.SetOptions(EntityOptions);
                return await factory(token);
            }, ct);

    public virtual async Task<TEntity?> GetByPropertyAsync(string prop, string val, Func<CancellationToken, Task<TEntity?>> factory, CancellationToken ct)
    {
        var normalizedVal = NormalizeCachedPropertyValue(val);

        var idxKey = IndexKey(prop, normalizedVal);

        if (cache.TryGetValue(idxKey, out TId? id) && id != null)
        {
            if (cache.TryGetValue(IdKey(id), out TEntity? entity))
                return entity;
        }

        var result = await factory(ct);
        if (result != null) SetEntity(result);
        return result;
    }

    public virtual void ResetEntity(TEntity entity)
    {
        cache.Remove(IdKey(GetId(entity)));

        foreach (var (prop, val) in GetCachedProperties(entity))
        {
            if (!string.IsNullOrWhiteSpace(val))
            {
                var normalizedVal = NormalizeCachedPropertyValue(val);
                cache.Remove(IndexKey(prop, normalizedVal));
            }
        }

        cache.Remove(AllKey);
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetOrCreateAllAsync(
        Func<CancellationToken, Task<IReadOnlyList<TEntity>>> factory,
        CancellationToken ct)
    {
        var result = await cache.GetOrCreateAsync(
            AllKey,
            async (entry, token) =>
            {
                entry.SetOptions(ListOptions);
                return await factory(token);
            }, ct);

        return result ?? [];
    }
    public virtual void RemoveByProperty(string prop, string val)
    {
        var normalizedVal = NormalizeCachedPropertyValue(val);
        cache.Remove(IndexKey(prop, normalizedVal));
    }
    public virtual async Task<IEnumerable<TEntity>> GetBySearchAsync(
        string searchTerm,
        Func<CancellationToken, Task<IEnumerable<TEntity>>> factory,
        CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return [];

        var normalizedTerm = NormalizeCachedPropertyValue(searchTerm);
        var searchKey = $"{Prefix}:search:{normalizedTerm}";

        return await cache.GetOrCreateAsync(searchKey, async (entry, token) =>
        {
            entry.SetOptions(ListOptions);

            var results = await factory(token);

            if (results != null)
            {
                foreach (var entity in results.Take(50))
                {
                    SetEntity(entity);
                }
            }

            return results;
        }, ct) ?? [];
    }
}
