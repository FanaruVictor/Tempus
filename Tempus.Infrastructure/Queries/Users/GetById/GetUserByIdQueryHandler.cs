using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Entities.User;
using Tempus.Core.IRepositories;
using Tempus.Core.Models.Photo;
using Tempus.Core.Models.User;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Queries.Users.GetById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, BaseResponse<UserDetails>>
{
    private readonly IUserPhotoRepository _userPhotoRepository;
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository, IUserPhotoRepository userPhotoRepository)
    {
        _userRepository = userRepository;
        _userPhotoRepository = userPhotoRepository;
    }

    public async Task<BaseResponse<UserDetails>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await _userRepository.GetById(request.UserId);

            if(user == null)
            {
                return BaseResponse<UserDetails>.NotFound("User not found.");
            }

            var profilePhoto = await _userPhotoRepository.GetByUserId(request.UserId);

            var userDetails = GenericMapper<User, UserDetails>.Map(user);

            if(profilePhoto != null)
            {
                userDetails.Photo = GenericMapper<Core.Entities.User.UserPhoto, PhotoDetails>.Map(profilePhoto);
            }

            var result = BaseResponse<UserDetails>.Ok(userDetails);

            return result;
        }
        catch(Exception exception)
        {
            var result = BaseResponse<UserDetails>.BadRequest(new List<string> {exception.Message});
            return result;
        }
    }
}