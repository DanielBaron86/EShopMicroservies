using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;
using Scalar.AspNetCore;

namespace BuildingBlocks.Extensions;

public static class OpenApiExtensions
{
    public static IServiceCollection AddOpenApiServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, context, ct) =>
            {
                document.Info = new() { Title = "API", Version = "v1" };
                
                document.Components ??= new();
                document.Components.SecuritySchemes = new Dictionary<string, IOpenApiSecurityScheme>
                {
                    ["Bearer"] = new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        Description = "Enter your Auth0 JWT token"
                    }
                };
                
                return Task.CompletedTask;
            });
        });

        return services;
    }

    public static IApplicationBuilder UseOpenApiDocs(
        this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        return app;
    }
    
    public static RouteHandlerBuilder RequireJwtAuthorization(this RouteHandlerBuilder builder)
    {
        return builder
            .RequireAuthorization()
            .WithOpenApi(op =>
            {
                op.Security = new List<OpenApiSecurityRequirement>
                {
                    new() { [new OpenApiSecuritySchemeReference("Bearer")] = new List<string>() }
                };
                return op;
            });
    }
}