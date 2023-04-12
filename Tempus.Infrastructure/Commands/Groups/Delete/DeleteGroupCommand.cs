using Tempus.Core.Commons;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Commands.Groups.Delete;

public class DeleteGroupCommand : BaseRequest<BaseResponse<Guid>>
{
    public Guid Id { get; set; }
}