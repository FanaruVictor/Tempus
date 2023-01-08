using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.Registrations;

namespace Tempus.Infrastructure.Queries.Registrations.LastUpdated;

public class GetLastUpdatedRegsitrationQuery : IRequest<BaseResponse<BaseRegistration>>
{
}