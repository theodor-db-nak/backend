using Application.Courses.Inputs;
using Domain.Models.Courses;

namespace Application.Courses.Factories;

public class CourseEventFactory
{
    public static CourseEvent Create(CreateCourseEventInput input)
    {
        var id = Guid.NewGuid();
        var courseEvent = new CourseEvent(
            id,
            input.CourseId,
            input.EventLocationId,
            input.StartTime,
            input.EndTime
            );

        return courseEvent;
    }
}
