using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Auth0.Authentication;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuth0Authentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        
        var auth0Settings = configuration
                                .GetSection(Auth0Settings.SectionName)
                                .Get<Auth0Settings>()
                            ?? throw new InvalidOperationException($"{Auth0Settings.SectionName} configuration is missing.");

        if (string.IsNullOrWhiteSpace(auth0Settings.Domain))
            throw new InvalidOperationException($"{Auth0Settings.SectionName}:Domain is missing.");

        if (string.IsNullOrWhiteSpace(auth0Settings.Audience))
            throw new InvalidOperationException($"{Auth0Settings.SectionName}:Audience is missing.");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                // This results in: https://tmsapi-danielsplaygrounds.us.auth0.com/
                options.Authority = $"https://{auth0Settings.Domain}/"; 
                options.Audience = auth0Settings.Audience;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = $"https://{auth0Settings.Domain}/", // Explicitly match the token 'iss'
                    ValidateIssuer = true,
                    NameClaimType = ClaimTypes.Name,
                    RoleClaimType = ClaimTypes.Role
                };
            });

        return services;
    }

    public static IServiceCollection AddAuth0Authorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("authenticated", policy =>
                policy.RequireAuthenticatedUser());
        });

        return services;
    }
}