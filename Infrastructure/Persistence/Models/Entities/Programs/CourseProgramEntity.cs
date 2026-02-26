using Infrastructure.Persistence.Models.Entities.Courses;

namespace Infrastructure.Persistence.Models.Entities.Programs;

public sealed class CourseProgramEntity
{
    public required Guid CourseId { get; set; }
    public required Guid ProgramId { get; set; }
    public CourseEntity Course { get; set; } = null!;
    public ProgramEntity Program { get; set; } = null!;
}
