using Domain.Common.Validation;

namespace Domain.Models.Courses;

public sealed class CourseModel
{

    public CourseModel(Guid id, string name)
    {
        Guard.AgainstEmptyGuid(id, "Id cannot be empty.");

        Guard.AgainstInvalidStr(name, 100, "Name");

        Id = id;
        Name = name.Trim();
    }
    public Guid Id { get; private init; }
    public string Name { get; private set; }
    public void Rename(string newName)
    {
        Guard.AgainstInvalidStr(newName, 100, "New name");
        Name = newName.Trim();
    }

}
