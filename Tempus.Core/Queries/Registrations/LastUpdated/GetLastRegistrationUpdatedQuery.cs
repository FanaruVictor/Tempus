using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.Registrations;

namespace Tempus.Core.Queries.Registrations.LastUpdated;

public class GetLastUpdatedRegsitrationQuery : IRequest<BaseResponse<BaseRegistration>>
{
}