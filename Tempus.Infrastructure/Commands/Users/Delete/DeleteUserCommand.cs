using Tempus.Core.Commons;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Commands.Users.Delete;

public class DeleteUserCommand : BaseRequest<BaseResponse<Guid>>
{
    public Guid Id { get; init; }
}