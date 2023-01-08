using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Models.User;
using Tempus.Core.Repositories;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Queries.Users.GetAll;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, BaseResponse<List<BaseUser>>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<BaseResponse<List<BaseUser>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var users = await _userRepository.GetAll();

            var result =
                BaseResponse<List<BaseUser>>.Ok(users.Select(GenericMapper<User,BaseUser>.Map).ToList());
            return result;
        }
        catch (Exception exception)
        {
            var result = BaseResponse<List<BaseUser>>.BadRequest(new List<string>{exception.Message});
            return result;
        }
    }
}