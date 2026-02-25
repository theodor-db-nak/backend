using Infrastructure.Persistence.Models.Entities.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Classes;

public sealed class ClassCourseEventEntityConfiguration : IEntityTypeConfiguration<ClassCourseEventEntity>
{
    public void Configure(EntityTypeBuilder<ClassCourseEventEntity> e)
    {
        e.ToTable("ClassCourseEvents");

        e.HasKey(cce => new { cce.ClassId, cce.CourseEventId });

        e.Property(cce => cce.ClassId)
            .HasMaxLength(68);

        e.Property(cce => cce.CourseEventId)
            .HasMaxLength(68);

        e.HasOne(cce => cce.Class)
            .WithMany(c => c.ClassCourseEvents)
            .HasForeignKey(cce => cce.ClassId)
            .OnDelete(DeleteBehavior.Cascade);

        e.HasOne(cce => cce.CourseEvent)
            .WithMany(ce => ce.ClassCourseEvents)
            .HasForeignKey(r => r.CourseEventId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
