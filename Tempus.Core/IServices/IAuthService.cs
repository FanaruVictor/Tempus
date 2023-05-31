using Tempus.Core.Commons;
using Tempus.Core.Models.Auth;
using Tempus.Core.Models.User;

namespace Tempus.Core.IServices;

public interface IAuthService
{
    Task<BaseResponse<LoginResult>> Login(LoginCredentials credentials, CancellationToken cancellationToken);
}