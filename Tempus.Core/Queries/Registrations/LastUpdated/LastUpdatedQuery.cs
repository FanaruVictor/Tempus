using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.Registration;

namespace Tempus.Core.Queries.Registrations.LastUpdated;

public class LastUpdatedQuery : IRequest<BaseResponse<DetailedRegistration>>
{
}