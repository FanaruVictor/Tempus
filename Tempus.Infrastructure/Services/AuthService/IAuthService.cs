using Tempus.Core.Commons;
using Tempus.Infrastructure.Models.Auth;
using Tempus.Infrastructure.Models.User;

namespace Tempus.Infrastructure.Services.AuthService;

public interface IAuthService
{
    Task<BaseResponse<LoginResult>> Login(LoginCredentials credentials, CancellationToken cancellationToken);
    Task<BaseResponse<UserDetails>> Register(RegistrationData userInfo, CancellationToken cancellationToken);
}