namespace Application.Courses.Inputs;

public sealed record UpdateCourseEventInput(
    Guid Id,
    Guid CourseId,
    Guid EventLocationId,
    DateTime StartTime,
    DateTime EndTime
    );
