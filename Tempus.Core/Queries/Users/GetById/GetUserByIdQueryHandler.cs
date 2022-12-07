using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.User;
using Tempus.Core.Repositories;

namespace Tempus.Core.Queries.Users.GetById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, BaseResponse<BaseUser>>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<BaseResponse<BaseUser>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            var response = await _userRepository.GetById(request.Id);

            if (response == null) return BaseResponse<BaseUser>.BadRequest("User not found");

            var result = BaseResponse<BaseUser>.Ok(new BaseUser(response.Id, response.UserName, response.Email));
            return result;
        }
        catch (Exception exception)
        {
            var result = BaseResponse<BaseUser>.NotFound(exception.Message);
            return result;
        }
    }
}