using Infrastructure.Persistence.Configurations.Extensions;
using Infrastructure.Persistence.Models.Entities.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Categories;

public sealed class CategoryEntityConfiguration : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> e)
    {
        e.ToTable("Categories", t =>
        {
            t.HasCheckConstraint("CK_Categoriy_Dates", "[ModifiedAt] >= [CreatedAt]");
        });

        e.HasKey(c => c.Id);

        e.Property(c => c.Id)
            .HasMaxLength(68);

        e.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        e.HasIndex(c => c.Name);

        e.ConfigureAuditableEntity();

        e.HasMany(c => c.CategoryCourses)
            .WithOne(cc => cc.Category)
            .HasForeignKey(cc => cc.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
