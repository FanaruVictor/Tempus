using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.Registration;

namespace Tempus.Core.Queries.Registrations.GetById;

public class GetRegistrationByIdQuery : IRequest<BaseResponse<DetailedRegistration>>
{
    public Guid Id { get; init; }
}