using Application.Common.Results;
using Application.Courses.Inputs;
using Domain.Models.Courses;

namespace Application.Courses.Contracts;

public interface ICourseService
{
    Task<Result<Course?>> GetCourseByIdAsync(Guid id, CancellationToken ct = default);
    Task<Result<Course?>> CreateCourseAsync(CreateCourseInput input, CancellationToken ct = default);
    Task<Result<Course?>> UpdateCourseAsync(UpdateCourseInput input, CancellationToken ct = default);
    Task<Result<IReadOnlyList<Course>>> GetCoursesAsync(CancellationToken ct = default);
    Task<Result> DeleteCourseAsync(Guid id, CancellationToken ct = default);
    Task<Result<IReadOnlyList<Course>>> GetCourseNamesBySearchAsync(string searchTerm, CancellationToken ct = default);
}
