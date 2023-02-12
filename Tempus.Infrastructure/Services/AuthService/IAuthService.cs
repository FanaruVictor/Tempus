using Tempus.Core.Commons;
using Tempus.Infrastructure.Models.Auth;
using Tempus.Infrastructure.Models.User;

namespace Tempus.Infrastructure.Services.AuthService;

public interface IAuthService
{
    Task<BaseResponse<string>> Login(LoginCredentials credentials, CancellationToken cancellationToken);
    Task<BaseResponse<BaseUser>> Register(RegistrationData userInfo, CancellationToken cancellationToken);
}