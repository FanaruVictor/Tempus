using Tempus.Core.Commons;

namespace Tempus.Core.IServices.Auth;

public class LoginResult
{
    public UserDetails User { get; set; }
    public string AuthorizationToken { get; set; }
}