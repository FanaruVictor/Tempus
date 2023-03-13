using Tempus.Core.Commons;
using Tempus.Core.Models.Registrations;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Queries.Registrations.GetById;

public class GetRegistrationByIdQuery : BaseRequest<BaseResponse<RegistrationDetails>>
{
    public Guid Id { get; init; }
}