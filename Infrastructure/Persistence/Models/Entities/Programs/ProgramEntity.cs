using Infrastructure.Persistence.Models.Entities.Categories;
using Infrastructure.Persistence.Models.Entities.Classes;

namespace Infrastructure.Persistence.Models.Entities.Programs;

public sealed class ProgramEntity
{
    public required Guid Id { get; set; }
    public required Guid CategoryId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public byte[] RowVersion { get; set; } = null!;
    public required CategoryEntity Category { get; set; }
    public ICollection<ClassEntity> Classes { get; set; } = [];
    public ICollection<CourseProgramEntity> CoursePrograms { get; set; } = [];
}
