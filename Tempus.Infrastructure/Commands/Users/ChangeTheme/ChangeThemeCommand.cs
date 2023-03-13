using Tempus.Core.Commons;
using Tempus.Core.Models.User;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Commands.Users.ChangeTheme;

public class ChangeThemeCommand : BaseRequest<BaseResponse<UserDetails>>
{
    public bool IsDarkTheme { get; set; }
}