namespace BuildingBlocks.AuthRequests;

public interface IAuthorizedRequest
{
    string[] RequiredRoles { get; }
    string[] RequiredPermissions { get; }

    bool RequireAllRoles { get; }
    bool RequireAllPermissions { get; }
}