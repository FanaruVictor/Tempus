using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.Registration;
using Tempus.Core.Repositories;

namespace Tempus.Core.Queries.Registrations.GetById;

public class GetRegistrationByIdQueryHandler : IRequestHandler<GetRegistrationByIdQuery, BaseResponse<DetailedRegistration>>
{
    private readonly IRegistrationRepository _registrationRepository;

    public GetRegistrationByIdQueryHandler(IRegistrationRepository registrationRepository)
    {
        _registrationRepository = registrationRepository;
    }

    public async Task<BaseResponse<DetailedRegistration>> Handle(GetRegistrationByIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var registration = await _registrationRepository.GetById(request.Id);


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