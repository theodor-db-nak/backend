using Domain.Common.Extensions;
using Domain.Common.Validation;
using Domain.Models.Addresses;
using Domain.Models.Classes;
using Domain.Models.Courses;
using Domain.Models.Roles;
using Domain.Models.ValueObjects;

namespace Domain.Models.Profiles;

public sealed class Profile
{

    private readonly List<Role> _roles = [];
    private readonly List<Class> _classes= [];
    private readonly List<Course> _courses = [];

    public Profile(Guid id, string firstName, string lastName, Email email, string password, PhoneNumber? phoneNumber, ProfileAddress address)
    {
        Guard.AgainstEmptyGuid(id, "Id cannot be empty.");

        Guard.AgainstInvalidStr(firstName, 100, "First name");
        Guard.AgainstInvalidStr(lastName, 100, "Last name");

        Guard.AgainstInsecurePassword(password);

        ArgumentNullException.ThrowIfNull(email);
        ArgumentNullException.ThrowIfNull(address);

        Id = id;
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
        Address = address;
    }

    public Guid Id { get; private init; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Email Email { get; private set; }
    public string Password { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public ProfileAddress Address { get; private set; }
    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();
    public IReadOnlyCollection<Class> Classes => _classes.AsReadOnly();
    public IReadOnlyCollection<Course> Courses => _courses.AsReadOnly();

    public void AddCourse(Course courseModel)
        => _courses.UpsertOrRemove(courseModel, c => c.Id == courseModel.Id, isAdding: true);
    public void RemoveCourse(Guid courseId)
        => _courses.UpsertOrRemove(null, c => c.Id == courseId, isAdding: false);

    public void AddClass(Class classModel)
        => _classes.UpsertOrRemove(classModel, c => c.Id == classModel.Id, isAdding: true);
    public void RemoveClass(Guid classId)
        => _classes.UpsertOrRemove(null, c => c.Id == classId, isAdding: false);

    public void AddRole(Role roleModel)
        => _roles.UpsertOrRemove(roleModel, r => r.Id == roleModel.Id, isAdding: true);

    public void RemoveRole(Guid roleId)
        => _roles.UpsertOrRemove(null, r => r.Id == roleId, isAdding: false);

    public void UpdateProfile(string firstName, string lastName, PhoneNumber? phoneNumber)
    {
        Guard.AgainstInvalidStr(firstName, 100, "First name");
        Guard.AgainstInvalidStr(lastName, 100, "Last name");

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        PhoneNumber = phoneNumber;
    }
    public void UpdateAddress(Address newAddressDetails)
    {
        Address.Update(newAddressDetails);
    }
}
