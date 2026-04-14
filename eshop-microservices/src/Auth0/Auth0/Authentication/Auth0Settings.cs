
namespace Auth0.Authentication;
public sealed class Auth0Settings
{
    public const string SectionName = "Auth0";
    public string Domain { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
}