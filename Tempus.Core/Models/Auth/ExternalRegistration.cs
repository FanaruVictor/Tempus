namespace Tempus.Core.Models.Auth;

public class ExternalRegistration
{
    public string ExternalId { get; set; }
    public string Username { get; set; }
    public string PhotoUrl { get; set; }
    public string Email { get; set; }
}