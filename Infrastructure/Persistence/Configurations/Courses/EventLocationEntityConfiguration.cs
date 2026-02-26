using Infrastructure.Persistence.Configurations.Extensions;
using Infrastructure.Persistence.Models.Entities.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Courses;

public sealed class EventLocationEntityConfiguration : IEntityTypeConfiguration<EventLocationEntity>
{
    public void Configure(EntityTypeBuilder<EventLocationEntity> e)
    {
        e.ToTable("EventLocations", t =>
        {
            t.HasCheckConstraint("CK_Event_Location_Dates", "[ModifiedAt] >= [CreatedAt]");
            t.HasCheckConstraint("CK_Seat_Min", "[Seats] >= 0");

        });

        e.HasKey(el => el.Id);

        e.Property(el => el.Id)
            .HasMaxLength(68);

        e.Property(el => el.Name)
            .HasMaxLength(100)
            .IsRequired();

        e.Property(el => el.Seats)
            .IsRequired();

        e.HasIndex(el => el.Name);

        e.ConfigureAuditableEntity();

        e.HasMany(el => el.CourseEvents)
            .WithOne(ce => ce.EventLocation)
            .HasForeignKey(ce => ce.EventLocationId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
