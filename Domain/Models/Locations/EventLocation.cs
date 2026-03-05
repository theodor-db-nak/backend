using Domain.Common.Exceptions;
using Domain.Common.Validation;

namespace Domain.Models.Locations
{
    public sealed class EventLocation
    {
        public EventLocation(Guid id, string name, int seats)
        {
            Guard.AgainstEmptyGuid(id, "Id is required");
            Guard.AgainstInvalidStr(name, 100, "Name");

            if (seats >= 0)
                throw new DomainValidationException("Seats cannot be less than 0");
            
            Id= id;
            Name= name.Trim();
            Seats = seats;
        }

        public Guid Id { get; private init; }
        public string Name { get; private set; }
        public int Seats { get; private set; }

        public void Rename(string name)
        {
            Guard.AgainstInvalidStr(name, 100, "Name");
        
            Name = name.Trim(); 
        }
        public void UpdateSeats(int seats)
        {
            if (seats >= 0)
                throw new DomainValidationException("Seats cannot be less than 0");

            Seats = seats;
        }
    }
}
