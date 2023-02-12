using Tempus.Core.Commons;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.Registrations;

namespace Tempus.Infrastructure.Queries.Registrations.GetById;

public class GetRegistrationByIdQuery : BaseRequest<BaseResponse<BaseRegistration>>
{
    public Guid Id { get; init; }
}