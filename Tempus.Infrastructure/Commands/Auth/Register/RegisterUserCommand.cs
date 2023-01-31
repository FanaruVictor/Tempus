using Tempus.Infrastructure.Commons;
using Tempus.Core.Commons;
using Tempus.Infrastructure.Models.User;

namespace Tempus.Infrastructure.Commands.Auth.Register;

public class  RegisterUserCommand : BaseRequest<BaseResponse<BaseUser>>
{
    public string UserName { get; init; }
    public string Email { get; init; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
}