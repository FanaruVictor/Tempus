using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Core.Models.Photo;
using Tempus.Core.Models.User;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Queries.Users.GetDetails;

public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, BaseResponse<UserDetails>>
{
    private readonly IUserRepository _userRepository;
    private readonly IProfilePhotoRepository _profilePhotoRepository;

    public GetUserDetailsQueryHandler(IUserRepository userRepository, IProfilePhotoRepository profilePhotoRepository)
    {
        _userRepository = userRepository;
        _profilePhotoRepository = profilePhotoRepository;
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

            var profilePhoto = await _profilePhotoRepository.GetByUserId(user.Id);
            
            var userDetails = GenericMapper<User, UserDetails>.Map(user);

            if(profilePhoto != null)
            {
                userDetails.Photo = GenericMapper<Core.Entities.ProfilePhoto, PhotoDetails>.Map(profilePhoto);
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