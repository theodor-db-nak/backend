using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Extensions;

public static class InfrastructureServiceRegistrationExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        if (env.IsDevelopment())
            services.AddDbContext<CourseOnlineDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CouseOnlineDb")));
        else
            services.AddDbContext<CourseOnlineDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CouseOnlineDb")));
      
        return services;
    }
}
