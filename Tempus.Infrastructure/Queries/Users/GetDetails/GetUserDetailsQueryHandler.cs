using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Entities.User;
using Tempus.Core.IRepositories;
using Tempus.Core.Models.Photo;
using Tempus.Core.Models.User;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Queries.Users.GetDetails;

public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, BaseResponse<UserDetails>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserPhotoRepository _userPhotoRepository;

    public GetUserDetailsQueryHandler(IUserRepository userRepository, IUserPhotoRepository userPhotoRepository)
    {
        _userRepository = userRepository;
        _userPhotoRepository = userPhotoRepository;
    }

    public async Task<BaseResponse<UserDetails>> Handle(GetUserDetailsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userRepository.GetById(request.UserId);

            BaseResponse<UserDetails> result;

            if(user == null)
            {
                result = BaseResponse<UserDetails>.NotFound("User not found!");
                return result;
            }

            var profilePhoto = await _userPhotoRepository.GetByUserId(user.Id);
            
            var userDetails = GenericMapper<User, UserDetails>.Map(user);

            if(profilePhoto != null)
            {
                userDetails.Photo = GenericMapper<Core.Entities.User.UserPhoto, PhotoDetails>.Map(profilePhoto);
            }
            
            result = BaseResponse<UserDetails>.Ok(userDetails);

            return result;
        }
        catch(Exception exception)
        {
            return BaseResponse<UserDetails>.BadRequest(new List<string>
            {
                exception.Message
            });
        }
    }
}