using Application.Common.Results;
using Application.Courses.Inputs;
using Domain.Models.Classes;
using Domain.Models.Courses;

namespace Application.Courses.Contracts;

public interface ICourseEventService
{
    Task<Result<CourseEvent?>> GetCourseEventByIdAsync(Guid id, CancellationToken ct = default);
    Task<Result<CourseEvent?>> CreateCourseEventAsync(CreateCourseEventInput input, CancellationToken ct = default);
    Task<Result<CourseEvent?>> UpdateCourseEventAsync(UpdateCourseEventInput input, CancellationToken ct = default);
    Task<Result<IReadOnlyList<CourseEvent>>> GetCourseEventsAsync(CancellationToken ct = default);
    Task<Result> DeleteCourseEventAsync(Guid id, CancellationToken ct = default);
    Task<Result> AddClassToCourseEventAsync(Guid courseEventId, Class classModel, CancellationToken ct = default);
    Task<Result> RemoveClassToCourseEventAsync(Guid courseEventId, Guid classId, CancellationToken ct = default);
}
