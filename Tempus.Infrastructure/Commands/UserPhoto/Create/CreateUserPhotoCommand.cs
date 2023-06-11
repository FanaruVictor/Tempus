using Microsoft.AspNetCore.Http;
using Tempus.Core.Commons;
using Tempus.Core.Models;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Commands.UserPhoto.Create;

public class CreateUserPhotoCommand : BaseRequest<BaseResponse<PhotoDetails>>
{
    public IFormFile Image { get; set; }
    public Guid? GroupId { get; set; }
}