using Application.Common.Caching;
using Application.Courses.Contracts;
using Domain.Models.Courses;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Courses.Caching;

public class CourseEventCache(IMemoryCache cache) : CacheEntityBase<CourseEvent, Guid>(cache), ICourseEventCache
{
    protected override Guid GetId(CourseEvent entity) => entity.Id;
    public Task<IReadOnlyList<CourseEvent>> GetAllAsync(Func<CancellationToken, Task<IReadOnlyList<CourseEvent>>> factory, CancellationToken ct)
        => GetOrCreateAllAsync(factory, ct);
    public Task<CourseEvent?> GetByIdAsync(Guid id, Func<CancellationToken, Task<CourseEvent?>> factory, CancellationToken ct)
        => GetOrCreateByIdAsync(id, factory, ct);
}
