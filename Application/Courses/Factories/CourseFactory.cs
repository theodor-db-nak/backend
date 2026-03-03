using Application.Courses.Inputs;
using Domain.Models.Courses;

namespace Application.Courses.Factories;

public static class CourseFactory
{
    public static Course Create(CreateCourseInput input)
    {
        var id = Guid.NewGuid();
        var course = new Course(
            id,
            input.Name
            );
        return course;
    }
}
