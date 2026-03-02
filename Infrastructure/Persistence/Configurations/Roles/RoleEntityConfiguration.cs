using Infrastructure.Persistence.Configurations.Extensions;
using Infrastructure.Persistence.Models.Entities.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Roles;

public sealed class RoleEntityConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> e)
    {
        e.ToTable("Roles", t =>
        {
            t.HasCheckConstraint("CK_Role_Dates", "[ModifiedAt] >= [CreatedAt]");
        });

        e.HasKey(r => r.Id);

        e.Property(r => r.Id)
            .HasMaxLength(68)
            .IsRequired();

        e.Property(r => r.Name)
            .HasMaxLength(100);

        e.Property(r => r.Description)
            .HasMaxLength(300)
            .IsRequired(false);

        e.ConfigureAuditableEntity();

        e.HasIndex(p => p.Name)
            .IsUnique();

        e.HasMany(r => r.ProfileRoles)
            .WithOne(pr => pr.Role)
            .HasForeignKey(pr => pr.RoleId);

        e.HasMany(p => p.RolePermissions)
            .WithOne(rp => rp.Role)
            .HasForeignKey(rp => rp.RoleId);
    }
}
