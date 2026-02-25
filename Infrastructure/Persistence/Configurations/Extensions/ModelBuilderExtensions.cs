using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Models.ValueObjects;

namespace Infrastructure.Persistence.Configurations.Extensions;

public static class ModelBuilderExtensions
{
    public static void ConfigureAuditableEntity<T>(this EntityTypeBuilder<T> e)
        where T : class
    {
        e.Property("CreatedAt") 
            .HasColumnType("datetime2(0)")
            .HasDefaultValueSql("SYSUTCDATETIME()")
            .IsRequired();

        e.Property("ModifiedAt")
            .HasColumnType("datetime2(0)")
            .HasDefaultValueSql("SYSUTCDATETIME()")
            .IsRequired();

        e.Property("RowVersion")
            .IsRowVersion()
            .IsConcurrencyToken();
    }

    public static void ConfigureAddress<TEntity>(OwnedNavigationBuilder<TEntity, Address> a)
       where TEntity : class
    {
        a.Property(p => p.Street)
            .HasMaxLength(150)
            .IsRequired();

        a.Property(p => p.City)
            .HasMaxLength(100)
            .IsRequired();

        a.Property(p => p.PostalCode)
            .HasMaxLength(20)
            .IsRequired();

        a.Property(p => p.Country)
            .HasMaxLength(100)
            .IsRequired();
    }
}
