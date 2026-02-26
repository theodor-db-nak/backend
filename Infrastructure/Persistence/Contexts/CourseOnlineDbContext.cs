using Infrastructure.Persistence.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts;

public sealed class CourseOnlineDbContext(DbContextOptions<CourseOnlineDbContext> options) : DbContext(options)
{
    public DbSet<ProfileEntity> Profiles => Set<ProfileEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseOnlineDbContext).Assembly);
    }
}
