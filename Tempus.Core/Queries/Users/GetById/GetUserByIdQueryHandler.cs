using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
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
            var user = await _userRepository.GetById(request.Id);

            if (user == null) return BaseResponse<BaseUser>.NotFound("User not found.");

            var baseUser = GenericMapper<User, BaseUser>.Map(user);
            var result = BaseResponse<BaseUser>.Ok(baseUser);
            
            return result;
        }
        catch (Exception exception)
        {
            var result = BaseResponse<BaseUser>.BadRequest(new List<string>{exception.Message});
            return result;
        }
    }
}