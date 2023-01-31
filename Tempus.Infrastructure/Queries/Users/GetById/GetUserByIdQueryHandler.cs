using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.Photo;
using Tempus.Infrastructure.Models.User;

namespace Tempus.Infrastructure.Queries.Users.GetById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, BaseResponse<UserDetails>>
{
    private readonly IUserRepository _userRepository;
    private readonly IProfilePhotoRepository _profilePhotoRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository, IProfilePhotoRepository profilePhotoRepository)
    {
        _userRepository = userRepository;
        _profilePhotoRepository = profilePhotoRepository;
    }

    public async Task<BaseResponse<UserDetails>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await _userRepository.GetById(request.UserId);

            if (user == null) return BaseResponse<UserDetails>.NotFound("User not found.");

            var profilePhoto = await _profilePhotoRepository.GetByUserId(request.UserId);
            
            var userDetails = GenericMapper<User, UserDetails>.Map(user);

            if (profilePhoto != null)
            {
                userDetails.Photo = GenericMapper<Core.Entities.ProfilePhoto, PhotoDetails>.Map(profilePhoto);
            }
            
            var result = BaseResponse<UserDetails>.Ok(userDetails);
            
            return result;
        }
        catch (Exception exception)
        {
            var result = BaseResponse<UserDetails>.BadRequest(new List<string>{exception.Message});
            return result;
        }
    }
}