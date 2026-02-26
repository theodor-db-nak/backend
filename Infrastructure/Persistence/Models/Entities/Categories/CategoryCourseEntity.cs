using Infrastructure.Persistence.Models.Entities.Courses;

namespace Infrastructure.Persistence.Models.Entities.Categories;

public sealed class CategoryCourseEntity
{
    public required Guid CategoryId { get; set; }
    public required Guid CourseId { get; set; }
    public CategoryEntity Category { get; set; } = null!;
    public CourseEntity Course { get; set; } = null!;
}
