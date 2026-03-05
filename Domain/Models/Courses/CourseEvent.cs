using Domain.Common.Extensions;
using Domain.Common.Validation;
using Domain.Models.Classes;
using Domain.Models.Roles;
namespace Domain.Models.Courses;

public class CourseEvent
{
    private readonly List<Class> _classes = [];
    public CourseEvent(Guid id, Guid courseId, Guid eventLocationId, DateTime startTime, DateTime endTime)
    {
        Guard.AgainstEmptyGuid(id, "Id cannot be empty.");
        Guard.AgainstEmptyGuid(courseId, "Course id cannot be empty.");
        Guard.AgainstEmptyGuid(eventLocationId, "Event location id cannot be empty.");

        Guard.IsBeforeDate(startTime, endTime, "End time must be after start time.");

        Id = id;
        CourseId = courseId;
        EventLocationId = eventLocationId;
        
        StartTime = startTime;
        EndTime = endTime;
    }
    public Guid Id { get; private init; }
    public Guid CourseId { get; private set; }
    public Guid EventLocationId { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public IReadOnlyCollection<Class> Classes => _classes.AsReadOnly();
    
    public void AddClass(Class classModel)
        => _classes.AddOrRemove(classModel, c => c.Id == classModel.Id, isAdding: true);
    public void RemoveClass(Guid classId)
        => _classes.AddOrRemove(null, c => c.Id == classId, isAdding: false);

    public void UpdateCourse(Guid newCourseId)
    {
        Guard.TransferValidation(newCourseId, CourseId);

        CourseId = newCourseId;
    }
    public void UpdateEventLocation(Guid newEventLocationId)
    {
        Guard.TransferValidation(newEventLocationId, EventLocationId);

        EventLocationId = newEventLocationId;
    }
    public void UpdateTimes(DateTime? startTime, DateTime? endTime)
    {
        var startValue = startTime ?? StartTime;
        var endValue = endTime ?? EndTime;

        Guard.IsBeforeDate(startValue, endValue, "End time must be after start time.");

        if (startTime.HasValue)
            StartTime = startTime.Value;

        if (endTime.HasValue)
            EndTime = endTime.Value;
    }

}
