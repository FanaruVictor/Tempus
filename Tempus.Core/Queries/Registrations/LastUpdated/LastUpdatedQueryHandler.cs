using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.Registration;
using Tempus.Core.Repositories;

namespace Tempus.Core.Queries.Registrations.LastUpdated;

public class LastUpdatedQueryHandler : IRequestHandler<LastUpdatedQuery, BaseResponse<DetailedRegistration>>
{
    private readonly IRegistrationRepository _registrationRepository;

    public LastUpdatedQueryHandler(IRegistrationRepository registrationRepository)
    {
        _registrationRepository = registrationRepository;
    }
    public async Task<BaseResponse<DetailedRegistration>> Handle(LastUpdatedQuery request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var registration = await _registrationRepository.GetLastUpdated();

            if (registration == null) return BaseResponse<DetailedRegistration>.NotFound("Registration not found!");
            
            var response = BaseResponse<DetailedRegistration>.Ok(new DetailedRegistration
            {
                Id = registration.Id,
                Title = registration.Title,
                Content = registration.Content
            });
            return response;

        }
        catch (Exception exception)
        {
            var response = BaseResponse<DetailedRegistration>.BadRequest(exception.Message);
            return response;
        }
    }
}