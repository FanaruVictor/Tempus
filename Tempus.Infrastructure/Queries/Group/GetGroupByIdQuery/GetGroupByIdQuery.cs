using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.Group;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Queries.Group.GetGroupByIdQuery;

public class GetGroupByIdQuery : BaseRequest<BaseResponse<GroupDetails>>
{
    public Guid Id { get; set; }
}