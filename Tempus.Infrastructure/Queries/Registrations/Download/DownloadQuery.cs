using MediatR;
using Tempus.Core.Commons;

namespace Tempus.Infrastructure.Queries.Registrations.Download;

public class DownloadQuery : IRequest<BaseResponse<byte[]>>
{
    public Guid Id { get; set; }
}