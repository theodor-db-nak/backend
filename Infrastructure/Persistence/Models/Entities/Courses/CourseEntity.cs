using Infrastructure.Persistence.Models.Entities.Categories;
using Infrastructure.Persistence.Models.Entities.Classes;
using Infrastructure.Persistence.Models.Entities.Programs;

namespace Infrastructure.Persistence.Models.Entities.Courses;

public sealed class CourseEntity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public byte[] RowVersion { get; set; } = null!;
    public ICollection<CourseProgramEntity> CoursePrograms { get; set; } = [];
    public ICollection<CategoryCourseEntity> CategoryCourses { get; set; } = [];
    public ICollection<CourseProfileEntity> CourseProfiles { get; set; } = [];
    public ICollection<CourseEventEntity> CourseEvents { get; set; } = [];
}
