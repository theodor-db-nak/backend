using Application.Profiles.Contracts;
using Application.Profiles.Inputs;
using Domain.Models.Roles;
using Domain.Models.ValueObjects;
using PresentationApi.Models.Profiles;
using PresentationAPI.Endpoints;

namespace PresentationApi.EndPoints;

public static class MapProfilesEndpoint
{
    public static RouteGroupBuilder MapProfileEndpoint(this RouteGroupBuilder api)
    {
        var group = api.MapGroup("/profiles")
            .WithTags("Profiles");

        group.MapPost("", CreateProfile)
            .WithName("CreateProfile");

        group.MapGet("", GetProfiles)
            .WithName("GetProfiles");

        group.MapPut("/{id:Guid}", UpdateProfile)
            .WithName("UpdateProfile");

        group.MapGet("/search/{searchTerm}", GetProfileBySearchName)
            .WithName("GetProfilesByNameSearch");

        group.MapGet("/email/{email}", GetProfileByEmail)
            .WithName("GetProfileByEmail");

        group.MapGet("/{id:Guid}", GetProfileById)
            .WithName("GetProfileById");

        group.MapDelete("/{id:Guid}", DeleteProfile)
            .WithName("DeleteProfileById");

        group.MapPost("/{id}/roles", AddRoleToProfile)
            .WithName("AddRoleToProfile");

        group.MapDelete("/{id}/roles/{roleId:Guid}", RemoveRoleFromProfile)
            .WithName("RemoveProfileRole");

        return group;
    }
    private static async Task<IResult> RemoveRoleFromProfile(
    Guid id,
    Guid roleId,
    IProfileService service,
    CancellationToken ct)
    {
        var result = await service.RemoveRoleToProfileAsync(id, roleId, ct);

        if (!result.Success)
            return result.ToHttpResult();

        return Results.Ok(result.Value);
    }
    private static async Task<IResult> AddRoleToProfile(
    Guid id,
    Role role, 
    IProfileService service,
    CancellationToken ct)
    {
        var result = await service.AddRoleToProfileAsync(id, role, ct);

        if (!result.Success)
            return result.ToHttpResult();
        

        return Results.Ok(result.Value);
    }
    private static async Task<IResult> UpdateProfile(Guid id, UpdateProfileRequest request, IProfileService service, CancellationToken ct)
    {
        var updatedAddress = Address.Create(request.Street, request.City, request.PostalCode, request.Country);

        var updatedProfile = new UpdateProfileInput(
            id,
            request.FirstName,
            request.LastName,
            request?.PhoneNumber,
            updatedAddress
            );

        var result = await service.UpdateProfileAsync(updatedProfile, ct);

        if (!result.Success)
            return result.ToHttpResult();

        return Results.Ok(result.Value);
    }
    private static async Task<IResult> GetProfileBySearchName(string searchTerm, IProfileService service, CancellationToken ct)
    {
        var result = await service.GetProfilesByName(searchTerm, ct);

        if (!result.Success)
            return result.ToHttpResult();

        return Results.Ok(result.Value);
    }
    private static async Task<IResult> GetProfiles(IProfileService service, CancellationToken ct)
    {
        var result = await service.GetProfilesAsync(ct);

        if (!result.Success)
            return result.ToHttpResult();

        return Results.Ok(result.Value);
    }
    private static async Task<IResult> DeleteProfile(
        Guid id, 
        IProfileService service, 
        CancellationToken ct)
    {
        if (Guid.Empty == id)
            return Results.BadRequest("Id is required");

        var result = await service.DeleteProfileAsync(id, ct);
        if(!result.Successs)
            return result.ToHttpResult();

        return Results.NoContent();
    }
    private static async Task<IResult> CreateProfile(
        CreateProfileRequest request,
        IProfileService service,
        CancellationToken ct)
    {
        var input = new CreateProfileInput(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password,
            request.PhoneNumber,
            request.Street,
            request.City,
            request.PostalCode,
            request.Country
        );

        var result = await service.CreateProfileAsync(input, ct);
        if (!result.Success)
            return result.ToHttpResult();

        return Results.Created($"/profiles/{result.Value?.Id}", new { id = result.Value?.Id });
    }

    private static async Task<IResult> GetProfileByEmail(
    string email,
    IProfileService service,
    CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Results.BadRequest("Email is required");

        var result = await service.GetProfileByEmailAsync(email, ct);

        if (!result.Success)
            return result.ToHttpResult();

        return Results.Ok(result.Value);
    }
    private static async Task<IResult> GetProfileById(
    Guid id,
    IProfileService service,
    CancellationToken ct)
    {
        if (Guid.Empty == id)
            return Results.BadRequest("Id is required");

        var result = await service.GetProfileByIdAsync(id, ct);

        if (!result.Success)
            return result.ToHttpResult();

        return Results.Ok(result.Value);
    }
}
