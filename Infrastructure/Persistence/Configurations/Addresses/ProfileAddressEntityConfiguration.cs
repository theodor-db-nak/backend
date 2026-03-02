using Infrastructure.Persistence.Configurations.Extensions;
using Infrastructure.Persistence.Models.Entities.Addresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Addresses;

public sealed class ProfileAddressEntityConfiguration : IEntityTypeConfiguration<ProfileAddressEntity>
{
    public void Configure(EntityTypeBuilder<ProfileAddressEntity> e)
    {
        e.ToTable("ProfileAddresses", t =>
        {
            t.HasCheckConstraint("CK_Profile_Address_Dates", "[ModifiedAt] >= [CreatedAt]");
        });

        e.HasKey(pa => pa.Id);

        e.Property(pa => pa.Id)
            .HasMaxLength(68);

        e.OwnsOne(pa => pa.Address, (a) => ModelBuilderExtensions.ConfigureAddress(a));

        e.ConfigureAuditableEntity();

        e.HasOne(pa => pa.Profile)
            .WithOne(p => p.Address)
            .HasForeignKey<ProfileAddressEntity>(pa => pa.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
