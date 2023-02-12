namespace Tempus.Infrastructure.Models.Auth;

public class RegistrationData
{
    public string UserName { get; init; }
    public string Email { get; init; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
}