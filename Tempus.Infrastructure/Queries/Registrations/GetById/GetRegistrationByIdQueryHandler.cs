using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.Registrations;

namespace Tempus.Infrastructure.Queries.Registrations.GetById;

public class GetRegistrationByIdQueryHandler : IRequestHandler<GetRegistrationByIdQuery, BaseResponse<BaseRegistration>>
{
    private readonly IRegistrationRepository _registrationRepository;

    public GetRegistrationByIdQueryHandler(IRegistrationRepository registrationRepository)
    {
        _registrationRepository = registrationRepository;
    }

    public async Task<BaseResponse<BaseRegistration>> Handle(GetRegistrationByIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var registration = await _registrationRepository.GetById(request.Id);
            
            if (registration == null) return BaseResponse<BaseRegistration>.NotFound("Registration not found!");

            var response = BaseResponse<BaseRegistration>.Ok(GenericMapper<Registration, BaseRegistration>.Map(registration));
            return response;
        }
        catch (Exception exception)
        {
            var response = BaseResponse<BaseRegistration>.BadRequest(new List<string>{exception.Message});
            return response;
        }
    }
}