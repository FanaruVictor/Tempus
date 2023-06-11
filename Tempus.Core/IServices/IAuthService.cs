using Tempus.Core.Commons;
using Tempus.Core.IServices.Auth;

namespace Tempus.Core.IServices;

public interface IAuthService
{
    Task<BaseResponse<LoginResult>> Login(LoginCredentials credentials, CancellationToken cancellationToken);
}