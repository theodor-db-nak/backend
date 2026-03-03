using Domain.Common.Base;

namespace Domain.Models.Courses.Repositories;

public interface ICourseRepository : IRepositoryBase<Course, Guid>
{
    Task<IReadOnlyList<Course>> GetCourseNamesBySearchAsync(string courseName, CancellationToken ct);
    Task<Course?> GetByNameAsync(string courseName, CancellationToken ct);
}
