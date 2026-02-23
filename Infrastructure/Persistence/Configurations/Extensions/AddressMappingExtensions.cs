using Domain.Models.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Extensions;

public static class AddressMappingExtensions
{
    public static void ConfigureAddress<TEntity>(this OwnedNavigationBuilder<TEntity, Address> a)
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