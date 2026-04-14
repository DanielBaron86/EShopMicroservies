using BuildingBlocks.AuthRequests;
using BuildingBlocks.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace BuildingBlocks.Behaviours;

public sealed class AuthorizationBehaviour<TRequest, TResponse>(
    IHttpContextAccessor httpContextAccessor,
    IOptions<AuthorizationOptions> authorizationOptions)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly AuthorizationOptions _authorizationOptions = authorizationOptions.Value;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is not IAuthorizedRequest authRequest)
            return await next();

        var user = httpContextAccessor.HttpContext?.User;

        if (user?.Identity?.IsAuthenticated != true)
            throw new UnauthorizedRequestException("User is not authenticated.");

        if (authRequest.RequiredRoles.Length > 0)
        {
            var userRoles = user.Claims
                .Where(c => c.Type == _authorizationOptions.RolesClaimType)
                .Select(c => c.Value)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var roleChecks = authRequest.RequiredRoles
                .Select(requiredRole => userRoles.Contains(requiredRole))
                .ToArray();

            var rolesOk = authRequest.RequireAllRoles
                ? roleChecks.All(x => x)
                : roleChecks.Any(x => x);

            if (!rolesOk)
                throw new ForbiddenRequestException("User does not have the required role(s).");
        }

        if (authRequest.RequiredPermissions.Length > 0)
        {
            var permissions = user.Claims
                .Where(c => c.Type == _authorizationOptions.PermissionsClaimType)
                .Select(c => c.Value)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var scopes = user.Claims
                .Where(c => c.Type == _authorizationOptions.ScopeClaimType)
                .SelectMany(c => c.Value.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var permissionChecks = authRequest.RequiredPermissions
                .Select(required => permissions.Contains(required) || scopes.Contains(required))
                .ToArray();

            var permissionsOk = authRequest.RequireAllPermissions
                ? permissionChecks.All(x => x)
                : permissionChecks.Any(x => x);

            if (!permissionsOk)
                throw new ForbiddenRequestException("User does not have the required permission(s).");
        }

        return await next();
    }
}