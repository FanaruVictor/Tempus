using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MediatR;
using Microsoft.Extensions.Options;
using Tempus.Core.Commons;
using Tempus.Core.IRepositories;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.Photo;
using Tempus.Infrastructure.Services.Cloudynary;

namespace Tempus.Infrastructure.Commands.ProfilePhoto.AddProfilePhoto;

public class AddProfilePhotoCommandHandler : IRequestHandler<AddProfilePhotoCommand, BaseResponse<PhotoDetails>>
{
    private readonly IProfilePhotoRepository _profilePhotoRepository;
    private readonly ICloudinaryService _cloudinaryService;

    public AddProfilePhotoCommandHandler(IProfilePhotoRepository profilePhotoRepository, ICloudinaryService cloudinaryService)
    {
        _cloudinaryService = cloudinaryService;
        _profilePhotoRepository = profilePhotoRepository;
    }

    public async Task<BaseResponse<PhotoDetails>> Handle(AddProfilePhotoCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var uploadResult = await _cloudinaryService.Upload(request.Image);
            
            var profilePhoto = await AddPhoto(request, uploadResult);

            var photoDetails = GenericMapper<Core.Entities.ProfilePhoto, PhotoDetails>.Map(profilePhoto);

            var result = BaseResponse<PhotoDetails>.Ok(photoDetails);

            return result;
        }
        catch(Exception exception)
        {
            var result = BaseResponse<PhotoDetails>.BadRequest(new List<string> {exception.Message});
            return result;
        }
    }

    private async Task<Core.Entities.ProfilePhoto> AddPhoto(AddProfilePhotoCommand request, ImageUploadResult uploadResult)
    {
        var profilePhoto = new Core.Entities.ProfilePhoto
        {
            Url = uploadResult.Url.ToString(),
            PublicId = uploadResult.PublicId,
            UserId = request.UserId
        };

        await _profilePhotoRepository.Add(profilePhoto);
        await _profilePhotoRepository.SaveChanges();
        return profilePhoto;
    }
}