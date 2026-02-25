using Infrastructure.Persistence.Models.Entities.Addresses;

namespace Infrastructure.Persistence.Models.Entities.Classes;

public sealed class ClassLocationEntity
{
    public required Guid Id { get; set; }
    public Guid? ClassLocationAddressId { get; set; }
    public required string Name { get; set; }
    public required bool IsOnline { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime ModifiedAt { get; set; }
    public byte[] RowVersion { get; set; } = null!;
    public ClassLocationAddressEntity? ClassLocationAddress { get; set; }
    public ICollection<ClassEntity> Classes { get; set; } = [];
}
