using CloudinaryDotNet.Actions;
using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.IRepositories;
using Tempus.Core.Models.Photo;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Services.Cloudynary;

namespace Tempus.Infrastructure.Commands.ProfilePhoto.UpdateProfilePhoto;

public class UpdateProfilePhotoCommandHandler : IRequestHandler<UpdateProfilePhotoCommand, BaseResponse<PhotoDetails>>
{
    private readonly IProfilePhotoRepository _profilePhotoRepository;
    private readonly ICloudinaryService _cloudinaryService;

    public UpdateProfilePhotoCommandHandler(IProfilePhotoRepository profilePhotoRepository, ICloudinaryService cloudinaryService)
    {
        _profilePhotoRepository = profilePhotoRepository;
        _cloudinaryService = cloudinaryService;
    }
    
    public async Task<BaseResponse<PhotoDetails>> Handle(UpdateProfilePhotoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var authenticityResult = await VerifyAuthenticity(request);
            
            if(authenticityResult.StatusCode != StatusCodes.Ok)
            {
                return authenticityResult;
            }

            var profilePhoto = await UpdateProfilePhoto(request);

            await _profilePhotoRepository.SaveChanges();
            
            var photoDetails = GenericMapper<Core.Entities.ProfilePhoto, PhotoDetails>.Map(profilePhoto);
    
            var result = BaseResponse<PhotoDetails>.Ok(photoDetails);

            return result;
        }
        catch(Exception exception)
        {
            return BaseResponse<PhotoDetails>.BadRequest(new List<string>
            {
                exception.Message
            });
        }
    }

    private async Task<Core.Entities.ProfilePhoto> UpdateProfilePhoto(UpdateProfilePhotoCommand request)
    {
        await _cloudinaryService.DestroyUsingUserId(request.UserId);

        await _profilePhotoRepository.Delete(request.Id);

        var uploadResult = await _cloudinaryService.Upload(request.Image);

        var photo = await UpdatePhoto(request.UserId, uploadResult);

        return photo;
    }

    private async Task<BaseResponse<PhotoDetails>> VerifyAuthenticity(UpdateProfilePhotoCommand request)
    {
        var photo = await _profilePhotoRepository.GetByUserId(request.UserId);

        BaseResponse<PhotoDetails> result;

        if(photo == null)
        {
            result = BaseResponse<PhotoDetails>.NotFound("Photo not found");
                return result;
        }

        if(photo.UserId != request.UserId)
        {
            result = BaseResponse<PhotoDetails>.Forbbiden();
                return result;
        }

        return BaseResponse<PhotoDetails>.Ok();
    }
    
    private async Task<Core.Entities.ProfilePhoto> UpdatePhoto(Guid userId, ImageUploadResult uploadResult)
    {
        var profilePhoto = new Core.Entities.ProfilePhoto
        {
            Url = uploadResult.Url.ToString(),
            PublicId = uploadResult.PublicId,
            UserId = userId
        };

        await _profilePhotoRepository.Add(profilePhoto);
        return profilePhoto;
    }
}