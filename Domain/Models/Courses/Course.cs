using Domain.Common.Extensions;
using Domain.Common.Validation;
using Domain.Models.Categories;
using Domain.Models.Profiles;
using Domain.Models.Programs;

namespace Domain.Models.Courses;

public sealed class Course
{
    private readonly List<Profile> _profiles = [];
    private readonly List<Category> _categories = [];
    private readonly List<Program> _programs = [];
    public Course(Guid id, string name)
    {
        Guard.AgainstEmptyGuid(id, "Id cannot be empty.");

        Guard.AgainstInvalidStr(name, 100, "Name");

        Id = id;
        Name = name.Trim();
    }
    public Guid Id { get; private init; }
    public string Name { get; private set; }
    public IReadOnlyCollection<Profile> Profiles => _profiles.AsReadOnly();
    public IReadOnlyCollection<Category> Categories => _categories.AsReadOnly();
    public IReadOnlyCollection<Program> Programs => _programs.AsReadOnly();

    public void AddProfile(Profile profileModel)
    => _profiles.AddOrRemove(profileModel, p => p.Id == profileModel.Id, isAdding: true);
    public void RemoveProfile(Guid profileId)
    => _profiles.AddOrRemove(null, p => p.Id == profileId, isAdding: false);

    public void AddCategory(Category categoryModel)
    => _categories.AddOrRemove(categoryModel, c => c.Id == categoryModel.Id, isAdding: true);
    public void RemoveCategory(Guid categoryId)
    => _categories.AddOrRemove(null, c => c.Id == categoryId, isAdding: false);

    public void AddCategory(Program programModel)
    => _programs.AddOrRemove(programModel, p => p.Id == programModel.Id, isAdding: true);
    public void RemoveProgram(Guid categoryId)
    => _programs.AddOrRemove(null, p => p.Id == categoryId, isAdding: false);

    public void Rename(string newName)
    {
        Guard.AgainstInvalidStr(newName, 100, "New name");
        Name = newName.Trim();
    }

}
