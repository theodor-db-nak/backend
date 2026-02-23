using Domain.Models.ValueObjects;

namespace Infrastructure.Persistence.Models.Entities;

public sealed class ProfileAddressEntity
{
    public Guid Id { get; set; }
    public required Address Address { get; set; }
    public required DateTime CreatedAt { get; set; } 
    public required DateTime? ModifiedAt { get; set; }
    public byte[] RowVersion { get; set; } = null!;
    public Guid ProfileId { get; set; }
    public ProfileEntity Profile { get; set; } = null!;
}
    