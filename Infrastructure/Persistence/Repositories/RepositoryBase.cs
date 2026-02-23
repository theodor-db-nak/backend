
using Domain.Common.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public abstract class RepositoryBase<TModel, TId, TEntity, TDbContext> : IRepositoryBase<TModel, TId> where TEntity : class where TDbContext : DbContext
{
    public Task<TModel> AddAsync(TModel model, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<TModel>> GetAllAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<TModel?> GetByIdAsync(TId id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveAsync(TId id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<TModel?> UpdateAsync(TId id, TModel model, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
