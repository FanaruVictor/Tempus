using Tempus.Core.Models.User;

namespace Tempus.Core.Models.Auth;

public class LoginResult
{
    public Guid UserId { get; set; }
    public string AuthorizationToken { get; set; }
    public bool IsDarkTheme { get; set; }
    public string PhotoURL { get; set; }
}