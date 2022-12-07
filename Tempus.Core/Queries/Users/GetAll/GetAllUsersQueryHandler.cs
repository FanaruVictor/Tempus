using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.User;
using Tempus.Core.Repositories;

namespace Tempus.Core.Queries.Users.GetAll;

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
                BaseResponse<List<BaseUser>>.Ok(users.Select(x => new BaseUser(x.Id, x.UserName, x.Email)).ToList());
            return result;
        }
        catch (Exception exception)
        {
            var result = BaseResponse<List<BaseUser>>.BadRequest(exception.Message);
            return result;
        }
    }
}