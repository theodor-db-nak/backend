using Application.Common.Results;
using Application.Courses.Contracts;
using Application.Courses.Factories;
using Application.Courses.Inputs;
using Domain.Models.Courses;
using Domain.Models.Courses.Repositories;

namespace Application.Courses.Services;

public sealed class CourseService(ICourseCache cache, ICourseRepository courseRepo) : ICourseService
{
    public async Task<Result<Course?>> CreateCourseAsync(CreateCourseInput input, CancellationToken ct = default)
    {
        try
        {
            var existing = await courseRepo.GetByNameAsync(input.Name, ct);

            if (existing != null)
                return Result<Course?>.Error("A course with this name already exists.");

            var course = CourseFactory.Create(input);

            await courseRepo.AddAsync(course, ct);

            cache.SetEntity(course);

            return Result<Course?>.Ok(course);
        }
        catch (Exception ex)
        {
            return Result<Course?>.Error(ex.Message);
        }
    }

    public async Task<Result> DeleteCourseAsync(Guid id, CancellationToken ct = default)
    {
        if (Guid.Empty == id)
            return Result.BadRequest("Id is required");

        var existingCourse = await courseRepo.GetByIdAsync(id, ct);
        if (existingCourse is null)
            return Result.NotFound("Course was not found");

        var deleted = await courseRepo.RemoveAsync(id, ct);
        if (!deleted)
            Result.Error("Course was not deleted");

        cache.ResetEntity(existingCourse);

        return Result.Ok();
    }

    public async Task<Result<Course?>> GetCourseByIdAsync(Guid id, CancellationToken ct = default)
    {
        if (Guid.Empty == id)
            return Result<Course?>.BadRequest("Id is required");

        var course = await cache.GetByIdAsync(id, token => courseRepo.GetByIdAsync(id, token), ct);

        return course is null ?
            Result<Course?>.NotFound("Course not found")
            : Result<Course?>.Ok(course);
    
    }

    public async Task<Result<IReadOnlyList<Course>>> GetCourseNamesBySearchAsync(string searchTerm, CancellationToken ct = default)
    {
        var courses = await cache.GetCourseNamesBySearchAsync(searchTerm, token => courseRepo.GetCourseNamesBySearchAsync(searchTerm, token), ct);

        var courseList = courses.ToList().AsReadOnly() ?? [];

        return Result<IReadOnlyList<Course>>.Ok(courseList);
    }

    public async Task<Result<IReadOnlyList<Course>>> GetCoursesAsync(CancellationToken ct = default)
    {
        var courses = await cache.GetAllAsync(token => courseRepo.GetAllAsync(token), ct);

        var courseList = courses.ToList().AsReadOnly() ?? [];

        return Result<IReadOnlyList<Course>>.Ok(courseList);
    }

    public async Task<Result<Course?>> UpdateCourseAsync(UpdateCourseInput input, CancellationToken ct = default)
    {
        if (Guid.Empty == input.Id)
            return Result<Course?>.BadRequest($"{nameof(input.Id)} is required");

        var course = await courseRepo.GetByIdAsync(input.Id, ct);
        if (course is null)
            return Result<Course?>.NotFound("Course not found");

        course.Rename(input.Name);

        var updatedCourse = await courseRepo.UpdateAsync(course.Id, course, ct);

        if (updatedCourse is null)
            return Result<Course?>.Error("Course was not updated");

        cache.ResetEntity(updatedCourse);
        cache.SetEntity(updatedCourse);

        return Result<Course?>.Ok(updatedCourse);
    }
}
