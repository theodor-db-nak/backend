using Infrastructure.Persistence.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class RoleEntityConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> e)
    {
        e.ToTable("Roles");

        e.HasKey(r => r.Id);

        e.Property(r => r.Id)
            .HasMaxLength(68);

        e.Property(r => r.Name)
            .HasMaxLength(100)
            .IsRequired();

        e.Property(r => r.Description)
          .HasMaxLength(300)
          .IsRequired(false);

        e.Property(r => r.ModifiedAt)
            .HasColumnType("datetime2(0)")
            .HasDefaultValueSql("SYSUTCDATETIME()")
            .IsRequired();
        
        e.Property(r => r.CreatedAt)
          .HasColumnType("datetime2(0)")
          .HasDefaultValueSql("SYSUTCDATETIME()")
          .IsRequired();
        
        e.Property(r => r.RowVersion)
            .IsRowVersion()
            .IsConcurrencyToken();

        e.HasMany(r => r.ProfileRoles)
            .WithOne(r => r.Role)
            .HasForeignKey(r => r.RoleId);
    }
}
