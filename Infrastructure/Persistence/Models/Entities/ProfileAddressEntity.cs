using Domain.Models.ValueObjects;

namespace Infrastructure.Persistence.Models.Entities;

public sealed class ProfileAddressEntity
{
    public Guid Id { get; set; }
    public Address Address { get; set; } = null!;

    public DateTime CreatedAt { get; set; } 
    public DateTime? ModifiedAt { get; set; }
    public byte[] RowVersion { get; set; } = null!;
    
    public Guid ProfileId { get; set; }
    public ProfileEntity Profile { get; set; } = null!;
}
