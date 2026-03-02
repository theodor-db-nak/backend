namespace Infrastructure.Persistence.Models.Entities.Classes;

public sealed class ClassProfileEntity
{
    public required Guid ClassId { get; set; }
    public required Guid ProfileId { get; set; }
    public ClassEntity Class { get; set; } = null!;
    public ProfileEntity Profile { get; set; } = null!;
}
