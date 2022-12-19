using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Models.Registrations;
using Tempus.Core.Repositories;

namespace Tempus.Core.Queries.Registrations.LastUpdated;

public class GetLastRegistrationUpdatedQueryHandler : IRequestHandler<GetLastUpdatedRegsitrationQuery, BaseResponse<BaseRegistration>>
{
    private readonly IRegistrationRepository _registrationRepository;

    public GetLastRegistrationUpdatedQueryHandler(IRegistrationRepository registrationRepository)
    {
        _registrationRepository = registrationRepository;
    }
    public async Task<BaseResponse<BaseRegistration>> Handle(GetLastUpdatedRegsitrationQuery request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var registration = await _registrationRepository.GetLastUpdated();

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