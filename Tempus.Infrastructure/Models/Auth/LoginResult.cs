using Tempus.Infrastructure.Models.User;

namespace Tempus.Infrastructure.Models.Auth;

public class LoginResult
{
    public UserDetails User { get; set; }
    public string AuthorizationToken { get; set; }
}