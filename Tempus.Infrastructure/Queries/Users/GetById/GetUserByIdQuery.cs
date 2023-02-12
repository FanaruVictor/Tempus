using Tempus.Core.Commons;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.User;

namespace Tempus.Infrastructure.Queries.Users.GetById;

public class GetUserByIdQuery : BaseRequest<BaseResponse<UserDetails>> { }