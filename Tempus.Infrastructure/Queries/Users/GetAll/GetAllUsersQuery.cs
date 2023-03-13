using Tempus.Core.Commons;
using Tempus.Core.Models.User;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Queries.Users.GetAll;

public class GetAllUsersQuery : BaseRequest<BaseResponse<List<UserDetails>>> { }