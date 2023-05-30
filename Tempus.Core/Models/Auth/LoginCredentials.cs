namespace Tempus.Core.Models.Auth;

public class LoginCredentials
{
    public string Email { get; init; }
    public string? ExternalId { get; init; }
    public string? PhotoURL { get; init; }
}