namespace Tempus.Core.Models.Auth;

public class RegistrationData
{
    public string Email { get; init; }
    public string ExternalId { get; set; }
    public string PhotoURL { get; set; }
}
