using CloudinaryDotNet.Actions;
using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.IRepositories;
using Tempus.Core.Models;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Services.Cloudynary;

namespace Tempus.Infrastructure.Commands.UserPhoto.Create;

public class CreateUserPhotoCommandHandler : IRequestHandler<CreateUserPhotoCommand, BaseResponse<PhotoDetails>>
{
    private readonly IUserPhotoRepository _userPhotoRepository;
    private readonly ICloudinaryService _cloudinaryService;

    public CreateUserPhotoCommandHandler(IUserPhotoRepository userPhotoRepository, ICloudinaryService cloudinaryService)
    {
        _cloudinaryService = cloudinaryService;
        _userPhotoRepository = userPhotoRepository;
    }

    public async Task<BaseResponse<PhotoDetails>> Handle(CreateUserPhotoCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var uploadResult = await _cloudinaryService.Upload(request.Image);
            
            var profilePhoto = await AddPhoto(request, uploadResult);

            var photoDetails = GenericMapper<Core.Entities.User.UserPhoto, PhotoDetails>.Map(profilePhoto);

            var result = BaseResponse<PhotoDetails>.Ok(photoDetails);

            return result;
        }
        catch(Exception exception)
        {
            var result = BaseResponse<PhotoDetails>.BadRequest(new List<string> {exception.Message});
            return result;
        }
    }

    private async Task<Core.Entities.User.UserPhoto> AddPhoto(CreateUserPhotoCommand request, ImageUploadResult uploadResult)
    {
        var profilePhoto = new Core.Entities.User.UserPhoto
        {
            Url = uploadResult.Url.ToString(),
            PublicId = uploadResult.PublicId,
            UserId = request.UserId
        };

        await _userPhotoRepository.Add(profilePhoto);
        await _userPhotoRepository.SaveChanges();
        return profilePhoto;
    }
}