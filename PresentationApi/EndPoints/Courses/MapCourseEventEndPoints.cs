using Application.Courses.Contracts;
using Application.Courses.Inputs;
using Domain.Models.Classes;
using PresentationApi.Models.Course;
using PresentationAPI.Endpoints;

namespace PresentationApi.EndPoints.Courses;

public static class MapCourseEventEndPoints
{
    public static RouteGroupBuilder MapCourseEventEndpoint(this RouteGroupBuilder api)
    {
        var group = api.MapGroup("course-events")
            .WithTags("CourseEvents");

        group.MapPost("", CreateCourseEvent)
              .WithName("CreateCourseEvent");

        group.MapGet("/{id:Guid}", GetCourseEventById)
            .WithName("GetCourseEventById");

        group.MapGet("", GetCourseEvents)
            .WithName("GetCourseEvents");

        group.MapDelete("/{id:Guid}", DeleteCourseEventById)
            .WithName("DeleteCourseEventById");

        group.MapPut("/{id:Guid}", UpdateCourseEvent)
            .WithName("UpdateCourseEvent");

        group.MapPost("/{id}/classes", AddRoleToClassEvent)
            .WithName("AddClassToCourseEvent");

        group.MapDelete("/{id}/classes/{classId:Guid}", RemoveRoleFromClassEvent)
            .WithName("RemoveCOurseEnventClass");

        return group;
    }
    public static async Task<IResult> CreateCourseEvent(CreateCourseEventRequest request, ICourseEventService service, CancellationToken ct)
    {
        var input = new CreateCourseEventInput(request.CourseId, request.EventLocationId, request.StartTime, request.EndTime);

        var result = await service.CreateCourseEventAsync(input, ct);

        if (!result.Success)
            return result.ToHttpResult();

        return Results.Created($"/course-events/{result.Value?.Id}", new { id = result.Value?.Id });
    }
    public static async Task<IResult> GetCourseEventById(Guid id, ICourseEventService service, CancellationToken ct)
    {
        if (Guid.Empty == id)
            return Results.BadRequest("Id is required");

        var result = await service.GetCourseEventByIdAsync(id, ct);

        if (!result.Success)
            return result.ToHttpResult();

        return Results.Ok(result.Value);
    }
    public static async Task<IResult> GetCourseEvents(ICourseEventService service, CancellationToken ct)
    {
        var result = await service.GetCourseEventsAsync(ct);

        if (!result.Success)
            return result.ToHttpResult();

        return Results.Ok(result.Value);
    }

    public static async Task<IResult> DeleteCourseEventById(Guid id, ICourseEventService service, CancellationToken ct)
    {
        if (Guid.Empty == id)
            return Results.BadRequest("Id is required");

        var result = await service.DeleteCourseEventAsync(id, ct);

        if (!result.Success)
            return result.ToHttpResult();

        return Results.NoContent();
    }
    public static async Task<IResult> UpdateCourseEvent(Guid id, UpdateCourseEventRequest request, ICourseEventService service, CancellationToken ct)
    {
        var updatedCourseEvent = new UpdateCourseEventInput(
            id,
            request.CourseId,
            request.EventLocationId,
            request.StartTime,
            request.EndTime
            );

        var result = await service.UpdateCourseEventAsync(updatedCourseEvent, ct);

        if (!result.Success)
            return result.ToHttpResult();

        return Results.Ok();
    }
    private static async Task<IResult> RemoveRoleFromClassEvent(
    Guid id,
    Guid classId,
    ICourseEventService service,
    CancellationToken ct)
    {
        var result = await service.RemoveClassToCourseEventAsync(id, classId, ct);

        if (!result.Success)
            return result.ToHttpResult();

        return Results.Ok();
    }
    private static async Task<IResult> AddRoleToClassEvent(
    Guid id,
    Class classModel,
    ICourseEventService service,
    CancellationToken ct)
    {
        var result = await service.AddClassToCourseEventAsync(id, classModel, ct);

        if (!result.Success)
            return result.ToHttpResult();

        return Results.Ok();
    }
}
