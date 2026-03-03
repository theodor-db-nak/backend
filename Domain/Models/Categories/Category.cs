using Domain.Common.Validation;
using Domain.Models.Courses;

namespace Domain.Models.Categories;

public sealed class Category
{
    private readonly List<Course> _courses = [];
    public Category(Guid id, string name)
    {
        Guard.AgainstEmptyGuid(id, "Id cannot be empty.");

        Guard.AgainstInvalidStr(name, 100, "Name");

        Id = id;
        Name = name.Trim();
    }
    public Guid Id { get; private init; }
    public string Name { get; private set; }
    public IReadOnlyCollection<Course> Courses => _courses.AsReadOnly();

    public void Rename(string newName)
    {
        Guard.AgainstInvalidStr(newName, 100, "New name");
        Name = newName.Trim();
    }

}
