using Domain.Models.ValueObjects;
using Infrastructure.Persistence.Configurations.Extensions;
using Infrastructure.Persistence.Models.Entities;
using Infrastructure.Persistence.Models.Entities.Addresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class ProfileEntityConfiguration : IEntityTypeConfiguration<ProfileEntity>
{
    public void Configure(EntityTypeBuilder<ProfileEntity> e)
    {
        e.ToTable("Profiles", t =>
        {
            t.HasCheckConstraint("CK_Profiles_Email_NotEmpty", "LEN([Email]) > 0");
            t.HasCheckConstraint("CK_Profiles_PhoneNumber_NotEmpty", "[PhoneNumber] IS NULL OR LEN([PhoneNumber]) > 0");
            t.HasCheckConstraint("CK_Profiles_Dates", "[ModifiedAt] >= [CreatedAt]");
        });

        e.HasKey(p => p.Id);

        e.Property(p => p.Id)
            .HasMaxLength(68);

        e.Property(p => p.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        e.Property(p => p.LastName)
            .HasMaxLength(100)
            .IsRequired();

        e.Property(p => p.Email)
            .HasConversion(
            email => email.Value,     
            value => Email.Create(value))
            .HasMaxLength(320)
            .IsRequired()
            .IsUnicode(false);

        e.Property(p => p.PhoneNumber)
            .HasConversion(
                p => p != null ? p.Value : null,
                v => v != null ? PhoneNumber.Create(v) : null
            )
            .HasMaxLength(20)
            .IsRequired(false);

        e.Property(p => p.Password)
            .HasMaxLength(256)
            .IsRequired()
            .IsUnicode(false);

        e.ConfigureAuditableEntity();

        e.HasIndex(p => p.Email)
            .IsUnique();

        e.HasIndex(p => new {p.FirstName, p.LastName});

        e.HasOne(p => p.Address)
            .WithOne(a => a.Profile)
            .HasForeignKey<ProfileAddressEntity>(a => a.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        e.HasMany(p => p.ProfileRoles)
            .WithOne(pr => pr.Profile)
            .HasForeignKey(pr => pr.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        e.HasMany(p => p.CourseProfiles)
            .WithOne(cp => cp.Profile)
            .HasForeignKey(cp => cp.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        e.HasMany(p => p.ClassProfiles)
            .WithOne(cp => cp.Profile)
            .HasForeignKey(cp => cp.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
