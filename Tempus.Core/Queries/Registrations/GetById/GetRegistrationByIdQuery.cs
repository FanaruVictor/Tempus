using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.Registrations;

namespace Tempus.Core.Queries.Registrations.GetById;

public class GetRegistrationByIdQuery : IRequest<BaseResponse<BaseRegistration>>
{
    public Guid Id { get; init; }
}