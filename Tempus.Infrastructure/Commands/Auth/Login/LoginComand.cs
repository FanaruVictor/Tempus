using MediatR;
using Tempus.Core.Commons;

namespace Tempus.Infrastructure.Commands.Auth.Login;

public class LoginComand : IRequest<BaseResponse<string>>
{
    public string Username { get; set; }
    public string Password { get; set; }
}