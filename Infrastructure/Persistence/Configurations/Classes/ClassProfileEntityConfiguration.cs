using Infrastructure.Persistence.Models.Entities.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Classes;

public sealed class ClassProfileEntityConfiguration : IEntityTypeConfiguration<ClassProfileEntity>
{
    public void Configure(EntityTypeBuilder<ClassProfileEntity> e)
    {
        e.ToTable("ClassProfiles");

        e.HasKey(cp => new { cp.ClassId, cp.ProfileId});

        e.Property(cp => cp.ClassId)
            .HasMaxLength(68);

        e.Property(cp => cp.ProfileId)
            .HasMaxLength(68);

        e.HasOne(cce => cce.Class)
            .WithMany(c => c.ClassProfiles)
            .HasForeignKey(cce => cce.ClassId)
            .OnDelete(DeleteBehavior.Cascade);

        e.HasOne(cce => cce.Profile)
            .WithMany(ce => ce.ClassProfiles)
            .HasForeignKey(r => r.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
