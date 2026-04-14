using Auth0.Authentication;
using Carter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddAuth0Authentication(builder.Configuration);
builder.Services.AddAuth0Authorization();
builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapCarter();

app.MapGet("/", () => Results.Ok(new
{
    Service = "Auth0",
    Status = "Running"
}));

app.MapGet("/test", [Authorize] () => Results.Ok(new
{
    Message = "Authorized test endpoint"
}));

app.MapHealthChecks("/health", new HealthCheckOptions());

app.Run();