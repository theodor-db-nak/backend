using Infrastructure.Persistence.Models.Entities.Programs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Programs;

public sealed class CourseProgramEntityConfiguration : IEntityTypeConfiguration<CourseProgramEntity>
{
    public void Configure(EntityTypeBuilder<CourseProgramEntity> e)
    {
        e.ToTable("CoursePrograms");

        e.HasKey(cp => new {cp.ProgramId, cp.CourseId});

        e.Property(cp => cp.ProgramId)
            .HasMaxLength(68);

        e.Property(cp => cp.CourseId)
            .HasMaxLength(68);

        e.HasOne(cp => cp.Program)
            .WithMany(p => p.CoursePrograms)
            .HasForeignKey(p => p.ProgramId)
            .OnDelete(DeleteBehavior.Cascade);

        e.HasOne(cp => cp.Course)
            .WithMany(c => c.CoursePrograms)
            .HasForeignKey(c => c.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
