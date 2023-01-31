using Tempus.Core.Commons;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Commands.Auth.Login;

public class LoginComand : BaseRequest<BaseResponse<string>>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}