using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.IRepositories;

namespace Tempus.Infrastructure.Queries.ProfilePhoto.ProfilePhotoGetById;

public class ProfilePhotoGetByIdQueryHandler : IRequestHandler<ProfilePhotoGetByIdQuery, BaseResponse<string>>
{
    private readonly IProfilePhotoRepository _profilePhotoRepository;

    public ProfilePhotoGetByIdQueryHandler(IProfilePhotoRepository profilePhotoRepository)
    {
        _profilePhotoRepository = profilePhotoRepository;
    }

    public async Task<BaseResponse<string>> Handle(ProfilePhotoGetByIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var profilePhoto = await _profilePhotoRepository.GetById(request.Id);

            if(profilePhoto == null)
            {
                return BaseResponse<string>.NotFound("Profile photo not found");
            }

            if(request.UserId != profilePhoto.UserId)
            {
                return BaseResponse<string>.BadRequest(new List<string>
                {
                    "This profile photo is not allocated to the user who requested it."
                });
            }

            return BaseResponse<string>.Ok(profilePhoto.Url);
        }
        catch(Exception exception)
        {
            return BaseResponse<string>.BadRequest(new List<string>
            {
                exception.Message
            });
        }
    }
}