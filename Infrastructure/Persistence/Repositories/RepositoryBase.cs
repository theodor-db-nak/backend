
using Domain.Common.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public abstract class RepositoryBase<TModel, TId, TEntity, TDbContext>(TDbContext context) : IRepositoryBase<TModel, TId> where TEntity : class where TDbContext : DbContext
{
    protected readonly TDbContext _context = context;
    protected DbSet<TEntity> Set => _context.Set<TEntity>();

    protected abstract TEntity ToEntity(TModel model);
    protected abstract TModel ToModel(TEntity entity);

    public virtual async Task<TModel?> AddAsync(TModel model, CancellationToken ct = default)
    {
        try
        {
            var entity = ToEntity(model);
            await Set.AddAsync(entity, ct);
            var result = await _context.SaveChangesAsync(ct);

            return result <= 0 ? default : ToModel(entity);
        }
        catch
        {
           return default;
        }
    }

    public virtual async Task<IReadOnlyList<TModel>> GetAllAsync(CancellationToken ct = default)
    {
            var entities = await Set.AsNoTracking().ToListAsync(ct);
            return [.. entities.Select(ToModel)];
    }

    public virtual async Task<TModel?> GetByIdAsync(TId id, CancellationToken ct = default)
    {
        var entity = await Set.FindAsync([id], ct);
        return entity is null ? default : ToModel(entity);
    }

    public virtual async Task<bool> RemoveAsync(TId id, CancellationToken ct = default)
    {
        try { 
        var entity = await Set.FindAsync([id], ct);
        if (entity is null) return default;

        Set.Remove(entity);
        var result = await _context.SaveChangesAsync(ct);
        return result >= 1;
    }
        catch
        {
            return default;
        }
    }
    public virtual async Task<TModel?> UpdateAsync(TId id, TModel model, CancellationToken ct = default)
    {
        try
        {
            var entity = await Set.FindAsync([id], ct);
            if (entity is null) return default;

            var updated = ToEntity(model);

            _context.Entry(entity).CurrentValues.SetValues(updated);
            var result = await _context.SaveChangesAsync(ct);

            return result <= 0 ? default : ToModel(entity);
        }
        catch
        {
            return default;
        }
    }
}
