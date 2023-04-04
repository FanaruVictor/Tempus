using Tempus.Core.Commons;
using Tempus.Core.Models.Group;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Queries.Groups;

public class GetAllGroupsQuery : BaseRequest<BaseResponse<List<GroupOverview>>>
{
}