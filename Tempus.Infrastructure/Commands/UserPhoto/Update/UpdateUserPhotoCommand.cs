using Microsoft.AspNetCore.Http;
using Tempus.Core.Commons;
using Tempus.Core.Models;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Commands.UserPhoto.Update;

public class UpdateUserPhotoCommand : BaseRequest<BaseResponse<PhotoDetails>>
{
    public Guid Id { get; set; }
    public IFormFile Image { get; set; }
    public Guid? GroupId { get; set; }
}