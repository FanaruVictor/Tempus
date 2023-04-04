using Tempus.Core.Commons;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Queries.UserPhoto.GetById;

public class GetPhotoByIdQuery : BaseRequest<BaseResponse<string>>
{
    public Guid Id { get; set; }
}