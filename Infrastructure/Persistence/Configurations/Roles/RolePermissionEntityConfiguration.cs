using Infrastructure.Persistence.Models.Entities.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Roles;

public sealed class RolePermissionEntityConfiguration : IEntityTypeConfiguration<RolePermissionEntity>
{
    public void Configure(EntityTypeBuilder<RolePermissionEntity> e)
    {
        e.ToTable("RolePermissions");

        e.HasKey( rp => new {rp.RoleId, rp.PermissionId});

        e.Property(rp => rp.PermissionId)
            .HasMaxLength(68);

        e.Property(rp => rp.RoleId)
            .HasMaxLength(68);

        e.HasOne(rp => rp.Permission)
            .WithMany(p => p.RolePermissions)
            .HasForeignKey(p => p.PermissionId)
            .OnDelete(DeleteBehavior.Restrict);

        e.HasOne(rp => rp.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(r  =>  r.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
