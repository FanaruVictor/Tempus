using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.User;

namespace Tempus.Infrastructure.Commands.Auth.Register;

public class  RegisterUserCommand : IRequest<BaseResponse<BaseUser>>
{
    public string UserName { get; init; }
    public string Email { get; init; }
    public string Password { get; set; }
}