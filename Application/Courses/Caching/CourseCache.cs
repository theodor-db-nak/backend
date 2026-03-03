using Application.Common.Caching;
using Application.Courses.Contracts;
using Domain.Models.Courses;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Courses.Caching;

public sealed class CourseCache(IMemoryCache cache) : CacheEntityBase<Course, Guid>(cache), ICourseCache
{
    protected override Guid GetId(Course entity) => entity.Id;

    public Task<IReadOnlyList<Course>> GetAllAsync(Func<CancellationToken, Task<IReadOnlyList<Course>>> factory, CancellationToken ct)
        => GetOrCreateAllAsync(factory, ct);
    public Task<Course?> GetByIdAsync(Guid id, Func<CancellationToken, Task<Course?>> factory, CancellationToken ct)
        => GetOrCreateByIdAsync(id, factory, ct);
    public Task<IReadOnlyList<Course>> GetCourseNamesBySearchAsync(string searchTerm, Func<CancellationToken, Task<IReadOnlyList<Course>>> factory, CancellationToken ct)
        => GetBySearchAsync(searchTerm, factory, ct);
}
