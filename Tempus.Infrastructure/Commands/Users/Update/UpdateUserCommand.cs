using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.User;

namespace Tempus.Infrastructure.Commands.Users.Update;

public class UpdateUserCommand : IRequest<BaseResponse<BaseUser>>
{
    public Guid Id { get; init; }
    public string UserName { get; init; }
    public string Email { get; init; }
}