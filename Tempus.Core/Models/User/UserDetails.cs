using Tempus.Core.Models.Photo;

namespace Tempus.Core.Models.User;

public class UserDetails
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public PhotoDetails Photo { get; set; }
    public bool IsDarkTheme { get; set; }
    public string ExternalId { get; set; }
}