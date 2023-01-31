using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.User;

namespace Tempus.Infrastructure.Queries.Users.GetDetails;

public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, BaseResponse<UserDetails>>
{
    private readonly IUserRepository _userRepository;

    public GetUserDetailsQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<BaseResponse<UserDetails>> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userRepository.GetById(request.UserId);

            BaseResponse<UserDetails> result;

            if (user == null)
            {
                result = BaseResponse<UserDetails>.NotFound("User not found!");
                return result;
            }

            var userDetails = GenericMapper<User, UserDetails>.Map(user);
            result = BaseResponse<UserDetails>.Ok(userDetails);

            return result;
        }
        catch (Exception exception)
        {
            return BaseResponse<UserDetails>.BadRequest(new List<string>
            {
                exception.Message
            });
        }
    }
}