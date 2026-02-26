using Infrastructure.Persistence.Models.Entities.Categories;
using Infrastructure.Persistence.Models.Entities.Programs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Categories;

public sealed class CategoryCourseEntityConfiguration : IEntityTypeConfiguration<CategoryCourseEntity>
{
    public void Configure(EntityTypeBuilder<CategoryCourseEntity> e)
    {
        e.ToTable("CategoryCourses");

        e.HasKey(pc => new { pc.CourseId, pc.CategoryId });

        e.Property(cc => cc.CourseId)
            .HasMaxLength(68);

        e.Property(cc => cc.CategoryId)
            .HasMaxLength(68);

        e.HasOne(cc => cc.Course)
            .WithMany(co => co.CategoryCourses)
            .HasForeignKey(cc => cc.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        e.HasOne(cc => cc.Category)
            .WithMany(ca => ca.CategoryCourses)
            .HasForeignKey(cc => cc.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
