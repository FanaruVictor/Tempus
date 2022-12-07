using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.User;

namespace Tempus.Core.Commands.Users.Update;

public class UpdateUserCommand : IRequest<BaseResponse<BaseUser>>
{
    public Guid Id { get; init; }
    public string UserName { get; init; }
    public string Email { get; init; }
}