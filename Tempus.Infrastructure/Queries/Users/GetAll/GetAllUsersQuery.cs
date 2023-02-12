using Tempus.Core.Commons;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.User;

namespace Tempus.Infrastructure.Queries.Users.GetAll;

public class GetAllUsersQuery : BaseRequest<BaseResponse<List<BaseUser>>> { }