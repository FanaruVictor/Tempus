using Tempus.Core.Commons;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Commands.UserPhoto.Delete;

public class DeleteUserPhotoCommand : BaseRequest<BaseResponse<bool>>
{
    public Guid Id { get; set; }
}