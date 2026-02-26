using Infrastructure.Persistence.Configurations.Extensions;
using Infrastructure.Persistence.Models.Entities.Addresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Addresses;

public sealed class ClassLocationAddressEntityConfiguration : IEntityTypeConfiguration<ClassLocationAddressEntity>
{
    public void Configure(EntityTypeBuilder<ClassLocationAddressEntity> e)
    {
        e.ToTable("ClassLocationAddresses", t =>
        {
            t.HasCheckConstraint("CK_Class_Location_Address_Dates", "[ModifiedAt] >= [CreatedAt]");
        });

        e.HasKey(cla => cla.Id);

        e.Property(cla => cla.Id)
            .HasMaxLength(68);

        e.OwnsOne(cla => cla.Address, (a) => ModelBuilderExtensions.ConfigureAddress(a));

        e.ConfigureAuditableEntity();

        e.HasMany(cla => cla.ClassLocations)
            .WithOne(cl => cl.ClassLocationAddress)
            .HasForeignKey(cl => cl.ClassLocationAddressId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
