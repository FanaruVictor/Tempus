using Tempus.Core.Commons;
using Tempus.Core.Models.Auth;
using Tempus.Core.Models.User;

namespace Tempus.Core.IServices;

public interface IAuthService
{
    Task<BaseResponse<LoginResult>> Login(LoginCredentials credentials, CancellationToken cancellationToken);
    Task<BaseResponse<UserDetails>> Register(RegistrationData userInfo, CancellationToken cancellationToken);
    Task<BaseResponse<LoginResult>> LoginWithGoogle(string googleCredentials, CancellationToken cancellationToken);
    Task<BaseResponse<LoginResult>> LoginWithFacebook(FacebookResponse facebookResponse, CancellationToken cancellationToken);
}