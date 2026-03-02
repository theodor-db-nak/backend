using Infrastructure.Persistence.Models.Entities.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Courses;

public sealed class CourseProfileEntityConfiguration : IEntityTypeConfiguration<CourseProfileEntity>
{
    public void Configure(EntityTypeBuilder<CourseProfileEntity> e)
    {
        e.ToTable("CourseProfiles");

        e.HasKey(cp => new { cp.CourseId, cp.ProfileId });

        e.Property(cp => cp.CourseId)
            .HasMaxLength(68);

        e.Property(cp => cp.ProfileId)
            .HasMaxLength(68);

        e.HasOne(cp => cp.Profile)
            .WithMany(p  => p.CourseProfiles)
            .HasForeignKey(p => p.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        e.HasOne(cp => cp.Course)
            .WithMany(c => c.CourseProfiles)
            .HasForeignKey(c => c.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
