using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.User;

namespace Tempus.Infrastructure.Queries.Users.GetById;

public class GetUserByIdQuery : IRequest<BaseResponse<BaseUser>>
{
    public Guid Id { get; init; }
}