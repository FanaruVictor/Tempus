using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.IRepositories;

namespace Tempus.Infrastructure.Queries.UserPhoto.GetById;

public class GetPhotoByIdQueryHandler : IRequestHandler<GetPhotoByIdQuery, BaseResponse<string>>
{
    private readonly IUserPhotoRepository _userPhotoRepository;

    public GetPhotoByIdQueryHandler(IUserPhotoRepository userPhotoRepository)
    {
        _userPhotoRepository = userPhotoRepository;
    }

    public async Task<BaseResponse<string>> Handle(GetPhotoByIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var profilePhoto = await _userPhotoRepository.GetById(request.Id);

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