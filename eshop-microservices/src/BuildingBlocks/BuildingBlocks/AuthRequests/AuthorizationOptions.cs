
namespace BuildingBlocks.AuthRequests;
public sealed class AuthorizationOptions
{
    public string RolesClaimType { get; init; } = "https://tmsapi.danielsplaygrounds.com/roles";
    public string PermissionsClaimType { get; init; } = "permissions";
    public string ScopeClaimType { get; init; } = "scope";
}