using CloudinaryDotNet.Actions;
using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.IRepositories;
using Tempus.Core.Models;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Services.Cloudynary;

namespace Tempus.Infrastructure.Commands.UserPhoto.Update;

public class UpdateUserPhotoCommandHandler : IRequestHandler<UpdateUserPhotoCommand, BaseResponse<PhotoDetails>>
{
    private readonly IUserPhotoRepository _userPhotoRepository;
    private readonly ICloudinaryService _cloudinaryService;

    public UpdateUserPhotoCommandHandler(IUserPhotoRepository userPhotoRepository, ICloudinaryService cloudinaryService)
    {
        _userPhotoRepository = userPhotoRepository;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<BaseResponse<PhotoDetails>> Handle(UpdateUserPhotoCommand request,
        CancellationToken cancellationToken)
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

            await _userPhotoRepository.SaveChanges();

            var photoDetails = GenericMapper<Core.Entities.User.UserPhoto, PhotoDetails>.Map(profilePhoto);

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

    private async Task<Core.Entities.User.UserPhoto> UpdateProfilePhoto(UpdateUserPhotoCommand request)
    {
        await _cloudinaryService.DestroyUsingUserId(request.UserId);

        await _userPhotoRepository.Delete(request.Id);

        var uploadResult = await _cloudinaryService.Upload(request.Image);

        var photo = await UpdatePhoto(request.UserId, uploadResult);

        return photo;
    }

    private async Task<BaseResponse<PhotoDetails>> VerifyAuthenticity(UpdateUserPhotoCommand request)
    {
        var photo = await _userPhotoRepository.GetByUserId(request.UserId);

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

    private async Task<Core.Entities.User.UserPhoto> UpdatePhoto(Guid userId, ImageUploadResult uploadResult)
    {
        var profilePhoto = new Core.Entities.User.UserPhoto
        {
            Url = uploadResult.Url.ToString(),
            PublicId = uploadResult.PublicId,
            UserId = userId
        };

        await _userPhotoRepository.Add(profilePhoto);
        return profilePhoto;
    }
}