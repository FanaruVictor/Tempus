using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.IRepositories;
using Tempus.Infrastructure.Services.Cloudynary;

namespace Tempus.Infrastructure.Commands.ProfilePhoto.DeleteProfilePhoto;

public class DeleteProfilePhotoCommandHandler: IRequestHandler<DeleteProfilePhotoCommand, BaseResponse<bool>>
{
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IProfilePhotoRepository _profilePhotoRepository;

    public DeleteProfilePhotoCommandHandler(ICloudinaryService cloudinaryService, IProfilePhotoRepository profilePhotoRepository)
    {
        _cloudinaryService = cloudinaryService;
        _profilePhotoRepository = profilePhotoRepository;
    }
    
    public async Task<BaseResponse<bool>> Handle(DeleteProfilePhotoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            await _cloudinaryService.DestroyUsingUserId(request.UserId);

            await _profilePhotoRepository.Delete(request.Id);
            
            return BaseResponse<bool>.Ok(true);

        }
        catch (Exception exception)
        {
            return BaseResponse<bool>.BadRequest(new List<string>
            {
                exception.Message
            });
        }
    }
}