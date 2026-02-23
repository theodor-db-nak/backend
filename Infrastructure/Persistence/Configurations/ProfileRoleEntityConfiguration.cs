using Infrastructure.Persistence.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class ProfileRoleEntityConfiguration : IEntityTypeConfiguration<ProfileRoleEntity>
{
    public void Configure(EntityTypeBuilder<ProfileRoleEntity> e)
    {
        e.ToTable("ProfileRoles");

        e.HasKey(pr => new { pr.ProfileId, pr.RoleId });

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
