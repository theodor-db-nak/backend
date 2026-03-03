namespace Application.Courses.Inputs;

public sealed record UpdateCourseInput
(
    Guid Id,
    string Name
);
