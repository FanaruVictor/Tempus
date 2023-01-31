using Tempus.Core.Commons;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Queries.ProfilePhoto.ProfilePhotoGetById;

public class ProfilePhotoGetByIdQuery : BaseRequest<BaseResponse<string>>
{
    public Guid Id { get; set; }
}