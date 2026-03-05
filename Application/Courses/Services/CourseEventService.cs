using Application.Common.Results;
using Application.Courses.Contracts;
using Application.Courses.Factories;
using Application.Courses.Inputs;
using Domain.Common.Exceptions;
using Domain.Models.Classes;
using Domain.Models.Courses;
using Domain.Models.Courses.Repositories;

namespace Application.Courses.Services;

public sealed class CourseEventService(ICourseEventCache cache, ICourseEventRepository courseEventRepo) : ICourseEventService
{
    public async Task<Result> AddClassToCourseEventAsync(Guid courseEventId, Class classModel, CancellationToken ct = default)
    {
        var courseEvent = await cache.GetOrCreateByIdAsync(
            courseEventId,
            token => courseEventRepo.GetByIdAsync(courseEventId, token),
            ct);

        if (courseEvent is null)
            return Result.Error("Course event not found.");

        courseEvent.AddClass(classModel);

        var updatedProfile = await courseEventRepo.UpdateAsync(courseEventId, courseEvent, ct);

        if (updatedProfile is null)
            return Result.Error("Failed to update course event. It may have been deleted or a database error occurred.");

        cache.SetEntity(updatedProfile);

        return Result.Ok();
    }

    public async Task<Result<CourseEvent?>> CreateCourseEventAsync(CreateCourseEventInput input, CancellationToken ct = default)
    {
        try
        {
            var course = CourseEventFactory.Create(input);

            var result =  await courseEventRepo.AddAsync(course, ct);

            if (result is null)
                return Result<CourseEvent?>.BadRequest("Failed to create course event.");

            cache.SetEntity(result);

            return Result<CourseEvent?>.Ok(result);
        }
        catch (Exception ex)
        {
            return Result<CourseEvent?>.Error(ex.Message);
        }
    }

    public async Task<Result> DeleteCourseEventAsync(Guid id, CancellationToken ct = default)
    {
        if (Guid.Empty == id)
            return Result.BadRequest("Id is required");

        var existingCourse = await courseEventRepo.GetByIdAsync(id, ct);
        if (existingCourse is null)
            return Result.NotFound("Course event was not found");

        var deleted = await courseEventRepo.RemoveAsync(id, ct);
        if (!deleted)
            Result.Error("Course event was not deleted");

        cache.ResetEntity(existingCourse);

        return Result.Ok();
    }

    public async Task<Result<CourseEvent?>> GetCourseEventByIdAsync(Guid id, CancellationToken ct = default)
    {
        if (Guid.Empty == id)
            return Result<CourseEvent?>.BadRequest("Id is required");

        var courseEvent = await cache.GetByIdAsync(id, token => courseEventRepo.GetByIdAsync(id, token), ct);

        return courseEvent is null ?
            Result<CourseEvent?>.NotFound("Course event not found")
            : Result<CourseEvent?>.Ok(courseEvent);
    }

    public async Task<Result<IReadOnlyList<CourseEvent>>> GetCourseEventsAsync(CancellationToken ct = default)
    {
        var courseEvents = await cache.GetAllAsync(token => courseEventRepo.GetAllAsync(token), ct);

        var courseEventList = courseEvents.ToList().AsReadOnly() ?? [];

        return Result<IReadOnlyList<CourseEvent>>.Ok(courseEventList);
    }

    public async Task<Result> RemoveClassToCourseEventAsync(Guid courseEventId, Guid classId, CancellationToken ct = default)
    {
        var courseEvent = await courseEventRepo.GetByIdAsync(courseEventId, ct);

        if (courseEvent is null)
            return Result.NotFound("Course event not found.");

        courseEvent.RemoveClass(classId);

        var updatedProfile = await courseEventRepo.UpdateAsync(courseEventId, courseEvent, ct);

        if (updatedProfile is null)
            return Result.Error("Failed to remove class. Database update failed.");

        cache.SetEntity(updatedProfile);

        return Result.Ok();
    }

    public async Task<Result<CourseEvent?>> UpdateCourseEventAsync(UpdateCourseEventInput input, CancellationToken ct = default)
    {
        if (Guid.Empty == input.Id)
            return Result<CourseEvent?>.BadRequest($"{nameof(input.Id)} is required");

        var courseEvent = await courseEventRepo.GetByIdAsync(input.Id, ct);
        if (courseEvent is null)
            return Result<CourseEvent?>.NotFound("CourseEvent not found");

        try
        {
            
            courseEvent.UpdateEventLocation(input.EventLocationId);
            courseEvent.UpdateCourse(input.CourseId);
            courseEvent.UpdateTimes(input.StartTime, input.EndTime);

        } catch (DomainValidationException ex)
        {
            return Result<CourseEvent?>.Error(ex.Message);
        }

        var updatedCourseEvent = await courseEventRepo.UpdateAsync(courseEvent.Id, courseEvent, ct);

        if (updatedCourseEvent is null)
            return Result<CourseEvent?>.Error("Course was not updated");

        cache.ResetEntity(updatedCourseEvent);
        cache.SetEntity(updatedCourseEvent);

        return Result<CourseEvent?>.Ok(updatedCourseEvent);
    }
}
