using Application.Common.Results;
using Application.Profiles.Contracts;
using Application.Profiles.Factories;
using Application.Profiles.Inputs;
using Domain.Models.Profiles;
using Domain.Models.Profiles.Repositories;
using Domain.Models.Roles;
using Domain.Models.ValueObjects;

namespace Application.Profiles.Services;

public sealed class ProfileService(IProfileCache cache, IProfileRepository profileRepo) : IProfileService
{
    public async Task<Result<Profile?>> CreateProfileAsync(CreateProfileInput input, CancellationToken ct = default)
    {
        try
        {
            var existing = await profileRepo.GetByEmailAsync(input.Email, ct);
            if (existing != null)
                return Result<Profile?>.Error("A profile with this email already exists.");
            

            var profile = ProfileFactory.Create(input);

            await profileRepo.AddAsync(profile, ct);
            
            cache.SetEntity(profile);

            return Result<Profile?>.Ok(profile);
        }
        catch (Exception ex)
        {
            return Result<Profile?>.Error(ex.Message);
        }
    }

    public async Task<Result> DeleteProfileAsync(Guid id, CancellationToken ct = default)
    {
        if(Guid.Empty == id)
            return Result.BadRequest("Id is required");

        var existingProfile = await profileRepo.GetByIdAsync(id, ct);
        if (existingProfile is null)
            return Result.NotFound("Profile not found");

        var deleted = await profileRepo.RemoveAsync(id, ct);
        if (!deleted)
            return Result.Error("Profile was not deleted");

        cache.ResetEntity(existingProfile);

        return Result.Ok();
    }

    public async Task<Result<Profile?>> GetProfileByEmailAsync(Email email, CancellationToken ct = default)
    {
        try
        {
            var profile = await cache.GetByPropertyAsync(
                nameof(Profile.Email),
                email,
                token => profileRepo.GetByEmailAsync(email, token),
                ct
                );

            if (profile is null)
                return Result<Profile?>.NotFound("Account not found.");

            return Result<Profile?>.Ok(profile);
        }
        catch (Exception ex)
        {
            return Result<Profile?>.Error($"Error retrieving profile: {ex.Message}");
        }
    }

    public async Task<Result<Profile?>> GetProfileByIdAsync(Guid id, CancellationToken ct = default)
    {
        if (Guid.Empty == id)
            return Result<Profile?>.BadRequest($"Id is required");

        var instructor = await cache.GetByIdAsync(id, token => profileRepo.GetByIdAsync(id, token), ct);

        return instructor is null
            ? Result<Profile?>.NotFound($"Instructor with id {id} was not found")
            : Result<Profile?>.Ok(instructor);
    }

    public async Task<Result<IReadOnlyList<Profile>>> GetProfilesAsync(CancellationToken ct = default)
    {
        var profiles = await cache.GetAllAsync(token => profileRepo.GetAllAsync(token), ct);

        return Result<IReadOnlyList<Profile>>.Ok(profiles ?? []);
    }

    public async Task<Result<IReadOnlyList<Profile>>> GetProfilesByName(string searchTerm, CancellationToken ct = default)
    {
        var profiles = await cache.GetBySearchAsync(searchTerm,async token => await profileRepo.SearchByNameRawSqlAsync(searchTerm, token), ct);

        var profileList = profiles?.ToList().AsReadOnly() ?? [];

        return Result<IReadOnlyList<Profile>>.Ok(profileList);
    }

    public async Task<Result<Profile?>> UpdateProfileAsync(UpdateProfileInput input, CancellationToken ct)
    {
        if (Guid.Empty == input.Id)
            return Result<Profile?>.BadRequest($"{nameof(input.Id)} is required");

        var profile = await profileRepo.GetByIdAsync(input.Id, ct);
        if (profile is null)
            return Result<Profile?>.NotFound($"Profile not found.");

        profile.UpdateProfile(input.FirstName, input.LastName, input?.PhoneNumber!);
        
        if(input?.Address is not null)
            profile.UpdateAddress(input.Address);

        var updateProfile = await profileRepo.UpdateAsync(profile.Id, profile, ct);
        if (updateProfile is null)
            return Result<Profile?>.Error("Profile was not updated");

        cache.ResetEntity(updateProfile);
        cache.SetEntity(updateProfile);

        return Result<Profile?>.Ok(updateProfile);
    }
    public async Task<Result<Profile?>> AddRoleToProfileAsync(Guid profileId, Role role, CancellationToken ct = default)
    {
        var profile = await cache.GetOrCreateByIdAsync(
            profileId,
            token => profileRepo.GetByIdAsync(profileId, token),
            ct);

        if (profile is null)
            return Result<Profile?>.Error("Profile not found.");

        profile.AddRole(role);

        var updatedProfile = await profileRepo.UpdateAsync(profileId, profile, ct);

        if (updatedProfile is null)
            return Result<Profile?>.Error("Failed to update profile. It may have been deleted or a database error occurred.");

        cache.SetEntity(updatedProfile);

        return Result<Profile?>.Ok(profile);
    }

    public async Task<Result<Profile?>> RemoveRoleToProfileAsync(Guid profileId, Guid roleId, CancellationToken ct = default)
    {
        var profile = await profileRepo.GetByIdAsync(profileId, ct);

        if (profile is null)
            return Result<Profile?>.NotFound("Profile not found.");

        profile.RemoveRole(roleId);

        var updatedProfile = await profileRepo.UpdateAsync(profileId, profile, ct);

        if (updatedProfile is null)
            return Result<Profile?>.Error("Failed to remove role. Database update failed.");

        cache.SetEntity(updatedProfile);

        return Result<Profile?>.Ok(updatedProfile);
    }
}
