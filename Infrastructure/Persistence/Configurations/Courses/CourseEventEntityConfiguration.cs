using Infrastructure.Persistence.Configurations.Extensions;
using Infrastructure.Persistence.Models.Entities.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Courses;

public sealed class CourseEventEntityConfiguration : IEntityTypeConfiguration<CourseEventEntity>
{
    public void Configure(EntityTypeBuilder<CourseEventEntity> e)
    {
        e.ToTable("CourseEvents", t =>
        {
            t.HasCheckConstraint("CK_Course_Event_Dates", "[ModifiedAt] >= [CreatedAt]");
        });

        e.HasKey(ce => ce.Id);

        e.Property(ce => ce.Id)
            .HasMaxLength(68);

        e.Property(ce => ce.CourseId)
            .HasMaxLength(68).IsRequired(); 
        
        e.Property(ce => ce.EventLocationId)
            .HasMaxLength(68).IsRequired();

        e.Property(ce => ce.StartTime)
            .HasColumnType("datetime2(0)")
            .IsRequired();

        e.Property(ce => ce.EndTime)
            .HasColumnType("datetime2(0)")
            .IsRequired();

        e.ConfigureAuditableEntity();

        e.HasOne(ce => ce.Course)
            .WithMany(c => c.CourseEvents)
            .HasForeignKey(c => c.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        e.HasOne(el => el.EventLocation)
            .WithMany(ce => ce.CourseEvents)
            .HasForeignKey(ce => ce.EventLocationId)
            .OnDelete(DeleteBehavior.Restrict);

        e.HasMany(el => el.ClassCourseEvents)
            .WithOne(cce => cce.CourseEvent)
            .HasForeignKey(cce => cce.CourseEventId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
