using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Tempus.Infrastructure.Services.Cloudynary;

public interface ICloudinaryService
{
    Task<ImageUploadResult> Upload(IFormFile image);
    Task DestroyUsingUserId(Guid userId);
}