using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.User;

namespace Tempus.Core.Commands.Users.Create;

public class CreateUserCommand : IRequest<BaseResponse<BaseUser>>
{
    public string UserName { get; init; }
    public string Email { get; init; }
}