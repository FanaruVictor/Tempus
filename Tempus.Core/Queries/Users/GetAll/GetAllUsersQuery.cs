using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.User;

namespace Tempus.Core.Queries.Users.GetAll;

public class GetAllUsersQuery : IRequest<BaseResponse<List<BaseUser>>>
{
}