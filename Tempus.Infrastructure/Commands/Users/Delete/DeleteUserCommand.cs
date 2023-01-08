using MediatR;
using Tempus.Core.Commons;

namespace Tempus.Infrastructure.Commands.Users.Delete;

public class DeleteUserCommand : IRequest<BaseResponse<Guid>>
{
    public Guid Id { get; init; }
}