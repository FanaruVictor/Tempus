using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MediatR;
using Microsoft.Extensions.Options;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.Photo;

namespace Tempus.Infrastructure.Commands.ProfilePhoto.AddProfilePhoto;

public class AddProfilePhotoCommandHandler : IRequestHandler<AddProfilePhotoCommand, BaseResponse<PhotoDetails>>
{
    private readonly IProfilePhotoRepository _profilePhotoRepository;
    private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
    private readonly Cloudinary _cloudynary;


    public AddProfilePhotoCommandHandler(IProfilePhotoRepository profilePhotoRepository,
        IOptions<CloudinarySettings> cloudinaryConfig)
    {
        _profilePhotoRepository = profilePhotoRepository;
        _cloudinaryConfig = cloudinaryConfig;
        var account = new Account(
            _cloudinaryConfig.Value.CloudName,
            _cloudinaryConfig.Value.ApiKey,
            _cloudinaryConfig.Value.ApiSecret);
        

        _cloudynary = new Cloudinary(account);
       
    }

    public async Task<BaseResponse<PhotoDetails>> Handle(AddProfilePhotoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var file = request.File;

            if (file.Length < 0)
            { 
                return BaseResponse<PhotoDetails>.BadRequest(new List<string>
                {
                    "There is no image to be saved"
                });
            }

            ImageUploadResult uploadResult;
            
            await using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(Guid.NewGuid().ToString(), stream),
                    Transformation = new Transformation().Width(150).Height(150).Crop("fill").Gravity("face")
                };
                
                uploadResult = _cloudynary.Upload(uploadParams);
            }

            var profilePhoto = new Core.Entities.ProfilePhoto
            {
                Url = uploadResult.Url.ToString(),
                PublicId = uploadResult.PublicId,
                UserId = request.UserId
            };

            await _profilePhotoRepository.Add(profilePhoto);

            var profilePhotoData = GenericMapper<Core.Entities.ProfilePhoto, PhotoDetails>.Map(profilePhoto);
        
            var result = BaseResponse<PhotoDetails>.Ok(profilePhotoData);

            return result;
        }
        catch (Exception exception)
        {
            var result = BaseResponse<PhotoDetails>.BadRequest(new List<string> { exception.Message });
            return result;
        }
    }
}