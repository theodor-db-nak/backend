using Infrastructure.Persistence.Models.Entities.Programs;

namespace Infrastructure.Persistence.Models.Entities.Classes;

public sealed class ClassEntity 
{
    public required Guid Id { get; set; }
    public required Guid ProgramId { get; set; }
    public required Guid ClassLocationId { get; set; }
    public required string Name { get; set; }
    public int Seats { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime ModifiedAt { get; set; }
    public byte[] RowVersion { get; set; } = null!;
    public ClassLocationEntity ClassLocation { get; set; } = null!;
    public ProgramEntity Program { get; set; } = null!;
    public ICollection<ClassProfileEntity> ClassProfiles { get; set; } = [];
    public ICollection<ClassCourseEventEntity> ClassCourseEvents { get; set; } = [];
}
