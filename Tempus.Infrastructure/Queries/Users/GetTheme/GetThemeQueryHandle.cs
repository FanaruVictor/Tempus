using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.IRepositories;

namespace Tempus.Infrastructure.Queries.Users.GetTheme;

public class GetThemeQueryHandle : IRequestHandler<GetThemeQuery, BaseResponse<bool>>
{
    private readonly IUserRepository _userRepository;

    public GetThemeQueryHandle(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<BaseResponse<bool>> Handle(GetThemeQuery request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var theme = await _userRepository.GetTheme(request.UserId);

            BaseResponse<bool> result;

            if (!theme.HasValue)
            {
                result = BaseResponse<bool>.NotFound("Theme for this user not found");
                return result;
            }

            result = BaseResponse<bool>.Ok(theme.Value);

            return result;
        }
        catch (Exception exception)
        {
            return BaseResponse<bool>.BadRequest(new List<string> { exception.Message });
        }
    }
}