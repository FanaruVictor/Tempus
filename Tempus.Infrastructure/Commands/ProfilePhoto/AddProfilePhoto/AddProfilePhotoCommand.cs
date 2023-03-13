using Microsoft.AspNetCore.Http;
using Tempus.Core.Commons;
using Tempus.Core.Models.Photo;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Commands.ProfilePhoto.AddProfilePhoto;

public class AddProfilePhotoCommand : BaseRequest<BaseResponse<PhotoDetails>>
{
    public IFormFile Image { get; set; }
}