using Infrastructure.Persistence.Configurations.Extensions;
using Infrastructure.Persistence.Models.Entities.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Classes;

public sealed class ClassLocationEntityConfiguration : IEntityTypeConfiguration<ClassLocationEntity>
{
    public void Configure(EntityTypeBuilder<ClassLocationEntity> e)
    {
        e.ToTable("ClassLocations", t =>
        {
            t.HasCheckConstraint("CK_Class_Locations_Dates", "[ModifiedAt] >= [CreatedAt]");
        });

        e.HasKey(cl => cl.Id);

        e.Property(cl => cl.Id)
            .HasMaxLength(68);

        e.Property(cl => cl.ClassLocationAddressId)
            .HasMaxLength(68)
            .IsRequired();

        e.Property(cl => cl.IsOnline)
            .IsRequired();

        e.ConfigureAuditableEntity();

        e.HasMany(cl => cl.Classes)
            .WithOne(c => c.ClassLocation)
            .HasForeignKey(c => c.ClassLocationId)
            .OnDelete(DeleteBehavior.Restrict);

        e.HasOne(cl => cl.ClassLocationAddress)
            .WithMany(cla => cla.ClassLocations)
            .HasForeignKey(cla => cla.ClassLocationAddressId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
