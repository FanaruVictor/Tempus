using System.Text.RegularExpressions;
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
    private readonly IUserPhotoRepository _userPhotoRepository;
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IOptions<CloudinarySettings> cloudinaryConfig, IUserPhotoRepository userPhotoRepository)
    {
        _cloudinaryConfig = cloudinaryConfig;
        _userPhotoRepository = userPhotoRepository;
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
        return await UploadImage(stream);
    }
    
    

    public async Task DestroyUsingUserId(Guid userId)
    {
        var photo = await _userPhotoRepository.GetByUserId(userId);

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

    public async Task<Dictionary<string, string>> UploadRegistrationImages(MatchCollection images)
    {
        var result = new Dictionary<string, string>();
        IEnumerable<byte[]> photos = images.Select(x =>
        {
            var content = x.Value;
            return Convert.FromBase64String(content[content.LastIndexOf(',')..]);
        });

        foreach(var photo in photos)
        {
            var steam = new MemoryStream(photo);
            var uploadResult = await UploadImage(steam);
            
        }

        return result;
    }
    
    private async Task<ImageUploadResult> UploadImage(Stream stream)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(Guid.NewGuid().ToString(), stream),
            Transformation = new Transformation().Width(200).Height(200).Crop("fill").Gravity("face")
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        return uploadResult;
    }
}