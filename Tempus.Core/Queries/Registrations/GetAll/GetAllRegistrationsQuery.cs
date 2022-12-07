using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.Registration;

namespace Tempus.Core.Queries.Registrations.GetAll;

public class GetAllRegistrationsQuery : IRequest<BaseResponse<List<RegistrationInfo>>>
{
    public Guid? CategoryId => null;
}