namespace Infrastructure.Persistence.Models.Entities.Courses;

public sealed class EventLocationEntity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public int Seats { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime ModifiedAt { get; set; }
    public byte[] RowVersion { get; set; } = null!;
    public ICollection<CourseEventEntity> CourseEvents { get; set; } = [];
}
