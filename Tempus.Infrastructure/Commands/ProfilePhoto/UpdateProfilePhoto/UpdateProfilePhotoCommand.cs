using Microsoft.AspNetCore.Http;
using Tempus.Core.Commons;
using Tempus.Core.Models.Photo;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Commands.ProfilePhoto.UpdateProfilePhoto;

public class UpdateProfilePhotoCommand : BaseRequest<BaseResponse<PhotoDetails>>
{
    public Guid Id { get; set; }
    public IFormFile Image { get; set; }
}