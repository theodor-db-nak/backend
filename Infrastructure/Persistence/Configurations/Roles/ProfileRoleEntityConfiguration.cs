using Infrastructure.Persistence.Models.Entities.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Roles;

public sealed class ProfileRoleEntityConfiguration : IEntityTypeConfiguration<ProfileRoleEntity>
{
    public void Configure(EntityTypeBuilder<ProfileRoleEntity> e)
    {
        e.ToTable("ProfileRoles");

        e.HasKey(pr => new { pr.ProfileId, pr.RoleId });

        e.Property(pr => pr.RoleId)
            .HasMaxLength(68);

        e.Property(pr => pr.ProfileId)
            .HasMaxLength(68);

        e.HasOne(pr => pr.Profile)
            .WithMany(p => p.ProfileRoles)
            .HasForeignKey(pr => pr.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        e.HasOne(pr => pr.Role)
            .WithMany(r => r.ProfileRoles)
            .HasForeignKey(pr => pr.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
