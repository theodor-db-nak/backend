namespace PresentationApi.Models.Course;

public class UpdateCourseEventRequest
{
    public required Guid CourseId { get; init; }
    public required Guid EventLocationId { get; init; }
    public required DateTime StartTime { get; init; }
    public required DateTime EndTime { get; init; }
}
