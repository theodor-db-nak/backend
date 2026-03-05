using Domain.Models.Classes;
using Domain.Models.Courses;
using Domain.Models.Courses.Repositories;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Models.Entities.Classes;
using Infrastructure.Persistence.Models.Entities.Courses;

namespace Infrastructure.Persistence.Repositories.Courses;

public class CourseEventRepository(CourseOnlineDbContext context) : RepositoryBase<CourseEvent, Guid, CourseEventEntity, CourseOnlineDbContext>(context), ICourseEventRepository
{
    protected override CourseEventEntity ToEntity(CourseEvent model)
    {
        var entity = new CourseEventEntity
        {
            Id = model.Id,
            CourseId = model.CourseId,
            EventLocationId = model.EventLocationId,
            StartTime = model.StartTime,
            EndTime = model.EndTime,

            ClassCourseEvents = [.. model.Classes.Select(cce => new ClassCourseEventEntity
            {
                CourseEventId = model.Id,
                ClassId = cce.Id,
            })]
        };

        return entity;
    }

    protected override CourseEvent ToModel(CourseEventEntity entity)
    {
        var model = new CourseEvent(
            entity.Id,
            entity.CourseId,
            entity.EventLocationId,
            entity.StartTime,
            entity.EndTime
            );

        foreach (var cce in entity.ClassCourseEvents)
            if (cce.Class != null)
                model.AddClass(new Class(cce.Class.Id, cce.Class.ProgramId, cce.Class.ClassLocationId, cce.Class.Name, cce.Class.Seats));

        return model;
    }
}
