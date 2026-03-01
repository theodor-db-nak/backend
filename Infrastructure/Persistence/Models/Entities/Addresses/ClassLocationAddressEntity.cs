using Domain.Models.ValueObjects;
using Infrastructure.Persistence.Models.Entities.Classes;

namespace Infrastructure.Persistence.Models.Entities.Addresses;

public sealed class ClassLocationAddressEntity
{
    public Guid Id { get; set; }
    public required Address Address { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public byte[] RowVersion { get; set; } = null!;
    public ICollection<ClassLocationEntity> ClassLocations { get; set; } = [];
}
