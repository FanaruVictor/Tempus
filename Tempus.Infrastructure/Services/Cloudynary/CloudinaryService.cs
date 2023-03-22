using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Tempus.Core.IRepositories;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Services.Cloudynary;

public class CloudinaryService : ICloudinaryService
{
    private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
    private readonly IProfilePhotoRepository _profilePhotoRepository;
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IOptions<CloudinarySettings> cloudinaryConfig, IProfilePhotoRepository profilePhotoRepository)
    {
        _cloudinaryConfig = cloudinaryConfig;
        _profilePhotoRepository = profilePhotoRepository;
        var account = new Account(
            _cloudinaryConfig.Value.CloudName,
            _cloudinaryConfig.Value.ApiKey,
            _cloudinaryConfig.Value.ApiSecret);

        _cloudinary = new Cloudinary(account);
    }
    
    public async Task<ImageUploadResult> Upload(IFormFile image)
    {
        if(image.Length < 0)
        {
            throw new Exception("Empty file");
        }

        await using var stream = image.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(Guid.NewGuid().ToString(), stream),
            Transformation = new Transformation().Width(200).Height(200).Crop("fill").Gravity("face")
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        return uploadResult;
    }

    public async Task DestroyUsingUserId(Guid userId)
    {
        var photo = await _profilePhotoRepository.GetByUserId(userId);

        if(photo == null)
        {
            throw new Exception("Photo not found");
        }
        
        var destroyParams = new DeletionParams(photo.PublicId)
        {
            ResourceType = ResourceType.Image,
        };

        await _cloudinary.DestroyAsync(destroyParams);
    }
}