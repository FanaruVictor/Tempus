using Microsoft.AspNetCore.Http;
using Tempus.Core.Commons;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.Photo;

namespace Tempus.Infrastructure.Commands.ProfilePhoto.AddProfilePhoto;

public class AddProfilePhotoCommand : BaseRequest<BaseResponse<PhotoDetails>>
{
    public IFormFile Image { get; set; }
}