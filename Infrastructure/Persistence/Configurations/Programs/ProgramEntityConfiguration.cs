using Infrastructure.Persistence.Configurations.Extensions;
using Infrastructure.Persistence.Models.Entities.Programs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Programs;

public sealed class ProgramEntityConfiguration : IEntityTypeConfiguration<ProgramEntity>
{
    public void Configure(EntityTypeBuilder<ProgramEntity> e)
    {
        e.ToTable("Programs", t =>
        {
            t.HasCheckConstraint("CK_Program_Dates", "[ModifiedAt] >= [CreatedAt]");
        });

        e.HasKey(p => p.Id);

        e.Property(p => p.Id)
            .HasMaxLength(68);

        e.Property(p => p.CategoryId)
            .HasMaxLength(68)
            .IsRequired();

        e.Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();

        e.Property(p => p.Description)
            .HasMaxLength(300)
            .IsRequired();

        e.ConfigureAuditableEntity();

        e.HasIndex(c => c.Name)
            .IsUnique();

        e.HasOne(p => p.Category)
            .WithMany(c => c.Programs)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        e.HasMany(p => p.Classes)
            .WithOne(c => c.Program)
            .HasForeignKey(c => c.ProgramId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
