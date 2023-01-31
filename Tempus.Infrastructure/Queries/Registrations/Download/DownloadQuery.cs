using Tempus.Core.Commons;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Queries.Registrations.Download;

public class DownloadQuery : BaseRequest<BaseResponse<byte[]>>
{
    public Guid Id { get; set; }
}