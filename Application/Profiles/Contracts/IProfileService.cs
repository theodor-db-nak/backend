using Application.Common.Results;
using Application.Profiles.Inputs;
using Domain.Models.Profiles;
using Domain.Models.Roles;
using Domain.Models.ValueObjects;

namespace Application.Profiles.Contracts;

public interface IProfileService
{
    Task<Result<Profile?>> CreateProfileAsync(CreateProfileInput input, CancellationToken ct = default);
    Task<Result<Profile?>> GetProfileByIdAsync(Guid id, CancellationToken ct = default);
    Task<Result<Profile?>> GetProfileByEmailAsync(Email email, CancellationToken ct = default);
    Task<Result<Profile?>> UpdateProfileAsync(UpdateProfileInput input ,CancellationToken ct);
    Task<Result<IReadOnlyList<Profile>>> GetProfilesAsync(CancellationToken ct = default);
    Task<Result> DeleteProfileAsync(Guid id, CancellationToken ct = default);
    Task<Result<IReadOnlyList<Profile>>> GetProfilesByName(string searchTerm, CancellationToken ct = default);
    Task<Result<Profile?>> AddRoleToProfileAsync(Guid profileId, Role role, CancellationToken ct = default);
    Task<Result<Profile?>> RemoveRoleToProfileAsync(Guid profileId, Guid roleId, CancellationToken ct = default);
}