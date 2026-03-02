using Infrastructure.Persistence.Configurations.Extensions;
using Infrastructure.Persistence.Models.Entities.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Courses;

public sealed class CourseEntityConfiguration : IEntityTypeConfiguration<CourseEntity>
{
    public void Configure(EntityTypeBuilder<CourseEntity> e)
    {
        e.ToTable("Courses", t =>
        {
            t.HasCheckConstraint("CK_Course_Dates", "[ModifiedAt] >= [CreatedAt]");
        });

        e.HasKey(c => c.Id);

        e.Property(c => c.Id)
            .HasMaxLength(68);

        e.Property(c => c.Name)
            .HasMaxLength(100);

        e.ConfigureAuditableEntity();

        e.HasIndex(c => c.Name)
            .IsUnique();

        e.HasMany(c => c.CoursePrograms)
            .WithOne(cp => cp.Course)
            .HasForeignKey(cp => cp.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        e.HasMany(c => c.CategoryCourses)
            .WithOne(cc => cc.Course)
            .HasForeignKey(cc => cc.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        e.HasMany(c => c.CourseProfiles)
            .WithOne(cp => cp.Course)
            .HasForeignKey(cp => cp.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
