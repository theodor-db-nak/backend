using Infrastructure.Persistence.Models.Entities.Programs;

namespace Infrastructure.Persistence.Models.Entities.Categories;

public sealed class CategoryEntity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime ModifiedAt { get; set; }
    public byte[] RowVersion { get; set; } = null!;
    public ICollection<ProgramEntity> Programs { get; set; } = [];
    public ICollection<CategoryCourseEntity> CategoryCourses { get; set; } = [];
}
