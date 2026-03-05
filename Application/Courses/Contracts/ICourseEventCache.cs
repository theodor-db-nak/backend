using Application.Common.Caching;
using Domain.Models.Courses;

namespace Application.Courses.Contracts;

public interface ICourseEventCache : ICacheEntityBase<CourseEvent, Guid>
{
    Task<IReadOnlyList<CourseEvent>> GetAllAsync(Func<CancellationToken, Task<IReadOnlyList<CourseEvent>>> factory, CancellationToken ct);
    Task<CourseEvent?> GetByIdAsync(Guid id, Func<CancellationToken, Task<CourseEvent?>> factory, CancellationToken ct);
}
