using Tempus.Core.Commons;
using Tempus.Core.Models.User;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Commands.Users.Update;

public class UpdateUserCommand : BaseRequest<BaseResponse<UserDetails>>
{
    public Guid Id { get; init; }
    public string UserName { get; init; }
    public string Email { get; init; }
}