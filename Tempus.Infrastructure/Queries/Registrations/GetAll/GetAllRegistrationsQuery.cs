using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.Registrations;

namespace Tempus.Infrastructure.Queries.Registrations.GetAll;

public class GetAllRegistrationsQuery : IRequest<BaseResponse<List<DetailedRegistration>>>
{
    public Guid? CategoryId { get; init; }
}