using Infrastructure.Persistence.Configurations.Extensions;
using Infrastructure.Persistence.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class ProfileAddressEntityConfiguration : IEntityTypeConfiguration<ProfileAddressEntity>
{
    public void Configure(EntityTypeBuilder<ProfileAddressEntity> e)
    {
        e.ToTable("ProfileAddresses");

        e.HasKey(pa => pa.Id);

        e.OwnsOne(pa => pa.Address, (e) => AddressMappingExtensions.ConfigureAddress(e));

        e.Property(pa => pa.ModifiedAt)
       .HasColumnType("datetime2(0)")
       .HasDefaultValueSql("SYSUTCDATETIME()")
       .IsRequired();

        e.Property(pa => pa.CreatedAt)
          .HasColumnType("datetime2(0)")
          .HasDefaultValueSql("SYSUTCDATETIME()")
          .IsRequired();

        e.Property(pa => pa.RowVersion)
            .IsRowVersion()
            .IsConcurrencyToken();

        e.HasOne(pa => pa.Profile)
            .WithOne(p => p.Address)
            .HasForeignKey<ProfileAddressEntity>(pa => pa.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
