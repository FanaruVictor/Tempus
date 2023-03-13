using Tempus.Core.Commons;
using Tempus.Core.Models.User;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Queries.Users.GetById;

public class GetUserByIdQuery : BaseRequest<BaseResponse<UserDetails>> { }