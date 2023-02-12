using Tempus.Core.Commons;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.User;

namespace Tempus.Infrastructure.Commands.Users.Update;

public class UpdateUserCommand : BaseRequest<BaseResponse<BaseUser>>
{
    public Guid Id { get; init; }
    public string UserName { get; init; }
    public string Email { get; init; }
}