using Infrastructure.Persistence.Configurations.Extensions;
using Infrastructure.Persistence.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class PermissionEntityConfiguration : IEntityTypeConfiguration<PermissionEntity>
{
    public void Configure(EntityTypeBuilder<PermissionEntity> e)
    {
        e.ToTable("Permissions", t =>
        {
            t.HasCheckConstraint("CK_Role_Dates", "[ModifiedAt] >= [CreatedAt]");
        });

        e.HasKey(p => p.Id);

        e.Property(p => p.Id)
            .HasMaxLength(68);

        e.Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();

        e.Property(p => p.Description)
            .HasMaxLength(200)
            .IsRequired(false);

        e.ConfigureAuditableEntity();

        e.HasIndex(p => p.Name)
            .IsUnique();

        e.HasMany(p => p.RolePermissions)
            .WithOne(rp => rp.Permission)
            .HasForeignKey(rp => rp.PermissionId);
    }
}
