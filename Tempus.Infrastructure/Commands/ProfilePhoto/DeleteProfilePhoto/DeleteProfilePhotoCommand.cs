using Tempus.Core.Commons;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Commands.ProfilePhoto.DeleteProfilePhoto;

public class DeleteProfilePhotoCommand : BaseRequest<BaseResponse<bool>>
{
    public Guid Id { get; set; }
}