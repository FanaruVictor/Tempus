using Tempus.Core.Commons;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.User;

namespace Tempus.Infrastructure.Commands.Users.ChangeTheme;

public class ChangeThemeCommand : BaseRequest<BaseResponse<UserDetails>>
{
    public bool Theme { get; set; }
}