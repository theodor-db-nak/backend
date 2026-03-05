namespace Application.Courses.Inputs;

public sealed record CreateCourseEventInput(
    Guid CourseId,
    Guid EventLocationId,
    DateTime StartTime,
    DateTime EndTime
    );
