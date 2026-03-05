using PresentationApi.EndPoints.Courses;
using PresentationApi.EndPoints.Profiles;

namespace PresentationApi.EndPoints;

public static class ApiEndpoints
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("/api");

        api.MapProfileEndpoint();
        api.MapCourseEndpoint();
        api.MapCourseEventEndpoint();

        return app;
    }
}
