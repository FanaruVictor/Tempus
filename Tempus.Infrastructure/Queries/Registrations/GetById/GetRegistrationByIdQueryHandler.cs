using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Core.Models.Registrations;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Queries.Registrations.GetById;

public class GetRegistrationByIdQueryHandler : IRequestHandler<GetRegistrationByIdQuery, BaseResponse<RegistrationDetails>>
{
    private readonly IRegistrationRepository _registrationRepository;

    public GetRegistrationByIdQueryHandler(IRegistrationRepository registrationRepository)
    {
        _registrationRepository = registrationRepository;
    }

    public async Task<BaseResponse<RegistrationDetails>> Handle(GetRegistrationByIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var registration = await _registrationRepository.GetById(request.Id);

            if(registration == null)
            {
                return BaseResponse<RegistrationDetails>.NotFound("Registration not found!");
            }
            
            var userId = registration.Category.UserCategories.FirstOrDefault(x => x.CategoryId == registration.Category.Id)
                ?.UserId;

            if(userId == null)
            {
                return BaseResponse<RegistrationDetails>.BadRequest(new List<string> {"Internal server error"});
            }
            
            if( userId != request.UserId)
            {
                return BaseResponse<RegistrationDetails>.Forbbiden();
            }

            var response =
                BaseResponse<RegistrationDetails>.Ok(GenericMapper<Registration, RegistrationDetails>.Map(registration));
            return response;
        }
        catch(Exception exception)
        {
            var response = BaseResponse<RegistrationDetails>.BadRequest(new List<string> {exception.Message});
            return response;
        }
    }
}