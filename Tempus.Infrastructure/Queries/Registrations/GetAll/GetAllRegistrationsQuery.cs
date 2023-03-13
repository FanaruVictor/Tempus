using Tempus.Core.Commons;
using Tempus.Core.Models.Registrations;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Queries.Registrations.GetAll;

public class GetAllRegistrationsQuery : BaseRequest<BaseResponse<List<RegistrationOverview>>>
{
    public Guid? CategoryId { get; init; }
}