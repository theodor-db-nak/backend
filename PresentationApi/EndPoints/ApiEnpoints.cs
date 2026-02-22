namespace PresentationApi.EndPoints;

public static class ApiEnpoints
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("/api");

        return app;
    }
}
