using Domain.Common.Exceptions;
using Domain.Common.Validation;

namespace Domain.Models.Classes;

public sealed class ClassModel
{
    public ClassModel(Guid id, string name, int seats)
    {
        Guard.AgainstEmptyGuid(id, "Id cannot be empty.");

        Guard.AgainstInvalidStr(name, 100, "Name");
        
        if (seats <= 0)
            throw new DomainValidationException("Seates cannot be negative.");

        Id = id;
        Name = name.Trim();
        Seats = seats;
    }
    public Guid Id { get; private init; }
    public Guid ProgramId { get; private set; }
    public Guid ClassLocationId { get; private set; }
    public string Name { get; private set; }
    public int Seats { get; private set; }

    public void Rename(string newName)
    {
        Guard.AgainstInvalidStr(newName, 100, "New name");

        Name = newName.Trim();
    }

    public void UpdateSeats(int newSeats)
    {
        if (newSeats < 0)
            throw new DomainValidationException("Seats cannot be negative.");

        Seats = newSeats;
    }

    public void TransferToProgram(Guid newProgramId)
    {
        Guard.TransferValidation(newProgramId, ProgramId);

        ProgramId = newProgramId;
    }
    public void TransferClassLocation(Guid newClassLocationId)
    {
        Guard.TransferValidation(newClassLocationId, ClassLocationId);

        ClassLocationId = newClassLocationId;
    }



}
