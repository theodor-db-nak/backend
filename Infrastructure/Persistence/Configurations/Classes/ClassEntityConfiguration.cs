using Infrastructure.Persistence.Configurations.Extensions;
using Infrastructure.Persistence.Models.Entities.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Classes;

public sealed class ClassEntityConfiguration : IEntityTypeConfiguration<ClassEntity>
{
    public void Configure(EntityTypeBuilder<ClassEntity> e)
    {
        e.ToTable("Classes", t =>
        {
            t.HasCheckConstraint("CK_Class_Dates", "[ModifiedAt] >= [CreatedAt]");
            t.HasCheckConstraint("CK_Seat_Min", "[Seats] >= 0");
        });

        e.HasKey(c => c.Id);

        e.Property(c => c.Id)
            .HasMaxLength(68);

        e.Property(c => c.ProgramId)
            .HasMaxLength(68)
            .IsRequired();

        e.Property(c => c.ClassLocationId)
            .HasMaxLength(68)
            .IsRequired();

        e.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        e.Property(c => c.Seats)
            .IsRequired();

        e.HasIndex(c => c.Name);

        e.ConfigureAuditableEntity();

        e.HasOne(c => c.ClassLocation)
            .WithMany(cl => cl.Classes)
            .HasForeignKey(c => c.ClassLocationId)
            .OnDelete(DeleteBehavior.Restrict);

        e.HasOne(c => c.Program)
            .WithMany(p => p.Classes)
            .HasForeignKey(c => c.ProgramId)
            .OnDelete(DeleteBehavior.Restrict);

        e.HasMany(c => c.ClassProfiles)
            .WithOne(cp => cp.Class)
            .HasForeignKey(cp => cp.ClassId)
            .OnDelete(DeleteBehavior.Cascade);

        e.HasMany(c => c.ClassCourseEvents)
            .WithOne(cp => cp.Class)
            .HasForeignKey(cp => cp.ClassId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
