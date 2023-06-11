namespace Tempus.Core.IServices.Auth;

public class RegistrationData
{
    public string UserName { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; set; }
    public string ExternalId { get; set; }
    public string PhotoURL { get; set; }
}
