using Domain.Models.ValueObjects;

namespace Infrastructure.Persistence.Models.Entities.Addresses;

public sealed class ProfileAddressEntity
{
    public Guid Id { get; set; }
    public required Address Address { get; set; }
    public DateTime CreatedAt { get; set; } 
    public DateTime ModifiedAt { get; set; }
    public byte[] RowVersion { get; set; } = null!;
    public Guid ProfileId { get; set; }
    public ProfileEntity Profile { get; set; } = null!;
}
    