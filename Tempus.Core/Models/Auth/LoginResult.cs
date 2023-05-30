using Tempus.Core.Models.User;

namespace Tempus.Core.Models.Auth;

public class LoginResult
{
    public UserDetails User { get; set; }
    public string AuthorizationToken { get; set; }
}