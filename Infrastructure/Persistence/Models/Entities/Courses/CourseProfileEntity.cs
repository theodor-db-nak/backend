namespace Infrastructure.Persistence.Models.Entities.Courses;

public sealed class CourseProfileEntity
{
    public required Guid CourseId { get; set; }
    public required Guid ProfileId { get; set; }
    public CourseEntity Course { get; set; } = null!;
    public ProfileEntity Profile { get; set; } = null!;
}
