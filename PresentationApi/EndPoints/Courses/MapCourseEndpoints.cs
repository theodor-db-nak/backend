using Application.Courses.Contracts;
using Application.Courses.Inputs;
using PresentationApi.Models.Course;
using PresentationAPI.Endpoints;

namespace PresentationApi.EndPoints.Courses
{
    public static class MapCourseEndpoints
    {
        public static RouteGroupBuilder MapCourseEndpoint(this RouteGroupBuilder api)
        {
            var group = api.MapGroup("courses")
                .WithTags("Courses");

            group.MapPost("", CreateCourse)
                .WithName("CreateCourse");

            group.MapGet("/{id:Guid}", GetCourseById)
                .WithName("GetCourseById");

            group.MapGet("", GetCourses)
                .WithName("GetCourses");

            group.MapGet("/search/{searchTerm}", GetCourseBySearch)
                .WithName("GetCourseBySearch");

            group.MapDelete("/{id:Guid}", DeleteCourseById)
                .WithName("DeleteCourseById");

            group.MapPut("/{id:Guid}", UpdateCourse)
                .WithName("UpdateCourse");

            return group;
        }
        public static async Task<IResult> CreateCourse(CreateCourseRequest request, ICourseService service, CancellationToken ct)
        {
            var input = new CreateCourseInput(request.Name);

            var result = await service.CreateCourseAsync(input, ct);

            if (!result.Success)
                return result.ToHttpResult();

            return Results.Created($"/courses/{result.Value?.Id}", new { id = result.Value?.Id });
        }

        public static async Task<IResult> GetCourseById(Guid id, ICourseService service, CancellationToken ct)
        {
            if(Guid.Empty == id)
                return Results.BadRequest("Id is required");

            var result = await service.GetCourseByIdAsync(id, ct);

            if (!result.Success)
                return result.ToHttpResult();

            return Results.Ok(result);
        }

        public static async Task<IResult> GetCourses(ICourseService service, CancellationToken ct)
        { 
            var result = await service.GetCoursesAsync(ct);

            if (!result.Success)
                return result.ToHttpResult();

            return Results.Ok(result.Value);
        }

        public static async Task<IResult> GetCourseBySearch(string searchTerm, ICourseService service, CancellationToken ct)
        {
            if(string.IsNullOrWhiteSpace(searchTerm))
                return Results.BadRequest("Search term is required");

            var result = await service.GetCourseNamesBySearchAsync(searchTerm, ct);

            if (!result.Success)
                return result.ToHttpResult();

            return Results.Ok(result.Value);
        }

        public static async Task<IResult> DeleteCourseById(Guid id, ICourseService service, CancellationToken ct)
        {
            if (Guid.Empty == id)
                return Results.BadRequest("Id is required");

            var result = await service.DeleteCourseAsync(id, ct);

            if(!result.Successs) 
                return result.ToHttpResult();

            return Results.NoContent();
        }

        public static async Task<IResult> UpdateCourse(Guid id, UpdateCourseRequest request, ICourseService service, CancellationToken ct)
        {
            var updatedCourse = new UpdateCourseInput(
                id,
                request.Name
                );

            var result = await service.UpdateCourseAsync(updatedCourse, ct);

            if (!result.Success)
                return result.ToHttpResult();

            return Results.Ok(new { id = result.Value?.Id, name = result.Value?.Name });
        }
    }
}
