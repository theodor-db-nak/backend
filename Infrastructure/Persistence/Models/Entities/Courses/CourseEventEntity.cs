using Infrastructure.Persistence.Models.Entities.Classes;

namespace Infrastructure.Persistence.Models.Entities.Courses;

public sealed class CourseEventEntity
{
    public required Guid Id { get; set; }
    public required Guid CourseId { get; set; }
    public required Guid EventLocationId { get; set; }
    public required DateTime StartTime { get; set;  }
    public required DateTime EndTime { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public byte[] RowVersion { get; set; } = null!;
    public EventLocationEntity EventLocation { get; set; } = null!;
    public CourseEntity Course { get; set; } = null!;
    public ICollection<ClassCourseEventEntity> ClassCourseEvents = [];
}
