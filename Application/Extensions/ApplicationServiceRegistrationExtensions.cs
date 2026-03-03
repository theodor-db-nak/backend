using Application.Courses.Caching;
using Application.Courses.Contracts;
using Application.Courses.Services;
using Application.Profiles.Caching;
using Application.Profiles.Contracts;
using Application.Profiles.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Application.Extensions;

public static class ApplicationServiceRegistrationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        services.AddMemoryCache();
        services.AddSingleton<IProfileCache, ProfileCache>();
        services.AddSingleton<ICourseCache, CourseCache>();

        services.AddScoped<IProfileService, ProfileService>();
        services.AddScoped<ICourseService, CourseService>();

        return services;
    }
}
