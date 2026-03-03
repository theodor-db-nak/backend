using Domain.Models.Courses;
using Domain.Models.Courses.Repositories;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Models.Entities.Courses;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Courses;

public sealed class CourseRepository(CourseOnlineDbContext context) : RepositoryBase<Course, Guid, CourseEntity, CourseOnlineDbContext>(context), ICourseRepository
{
    public async Task<IReadOnlyList<Course>> GetCourseNamesBySearchAsync(string courseName, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(courseName)) return [];

        var entities = await Set
            .Where(c => c.Name.Contains(courseName))
            .AsNoTracking()
            .ToListAsync(ct);

        return entities is null ? [] : [.. entities.Select(ToModel)];
    }
    public async Task<Course?> GetByNameAsync(string courseName, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(courseName)) return null;

        var entity = await Set
        .AsNoTracking()
        .FirstOrDefaultAsync(c => c.Name == courseName, ct);

        return entity is null ? default : ToModel(entity);
    }
    protected override CourseEntity ToEntity(Course model)
    {
        var entity = new CourseEntity {
           Id = model.Id,
           Name = model.Name,
           };

        return entity;
    }

    protected override Course ToModel(CourseEntity entity)
    {
        var model = new Course(
            entity.Id,
            entity.Name
        );

        return model;
    }
}
