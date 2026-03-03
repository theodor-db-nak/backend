using Domain.Common.Validation;

namespace Domain.Models.Programs;

public sealed class Program
{
    public Program(Guid id, Guid categoryId, string name, string? description)
    {
        Guard.AgainstEmptyGuid(id, "Id cannot be empty.");
        Guard.AgainstEmptyGuid(categoryId, "CategoryId cannot be empty.");

        Guard.AgainstInvalidStr(name, 100, "Name");
        Guard.MaxLengthValidation(description, 300, "Description");

        Id = id;
        CategoryId = categoryId;
        Name = name;
        Description = description;
    }

    public Guid Id { get; private init; }
    public Guid CategoryId { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
}
