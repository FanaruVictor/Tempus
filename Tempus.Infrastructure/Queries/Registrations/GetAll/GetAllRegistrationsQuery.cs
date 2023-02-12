using Tempus.Core.Commons;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.Registrations;

namespace Tempus.Infrastructure.Queries.Registrations.GetAll;

public class GetAllRegistrationsQuery : BaseRequest<BaseResponse<List<RegistrationOverview>>>
{
    public Guid? CategoryId { get; init; }
}