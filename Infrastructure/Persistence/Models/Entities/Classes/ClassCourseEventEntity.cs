using Infrastructure.Persistence.Models.Entities.Courses;

namespace Infrastructure.Persistence.Models.Entities.Classes;

public sealed class ClassCourseEventEntity
{
    public required Guid ClassId {  get; set; }
    public required Guid CourseEventId { get; set; }
    public ClassEntity Class { get; set; } = null!;
    public CourseEventEntity CourseEvent { get; set; } = null!;
}
