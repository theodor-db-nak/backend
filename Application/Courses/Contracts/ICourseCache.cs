using Application.Common.Caching;
using Domain.Models.Courses;

namespace Application.Courses.Contracts;

public interface ICourseCache : ICacheEntityBase<Course, Guid>
{
    Task<IReadOnlyList<Course>> GetAllAsync(Func<CancellationToken, Task<IReadOnlyList<Course>>> factory, CancellationToken ct);
    Task<IReadOnlyList<Course>> GetCourseNamesBySearchAsync(string searchTerm, Func<CancellationToken, Task<IReadOnlyList<Course>>> factory, CancellationToken ct);
    Task<Course?> GetByIdAsync(Guid id, Func<CancellationToken, Task<Course?>> factory, CancellationToken ct);
}
    