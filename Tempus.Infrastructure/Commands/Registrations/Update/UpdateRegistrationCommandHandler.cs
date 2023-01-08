using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Models.Registrations;
using Tempus.Core.Repositories;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Commands.Registrations.Update;

public class UpdateRegistrationCommandHandler : IRequestHandler<UpdateRegistrationCommand, BaseResponse<BaseRegistration>>
{
    private readonly IRegistrationRepository _registrationRepository;

    public UpdateRegistrationCommandHandler(IRegistrationRepository registrationRepository)
    {
        _registrationRepository = registrationRepository;
    }

    public async Task<BaseResponse<BaseRegistration>> Handle(UpdateRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            var entity = await _registrationRepository.GetById(request.Id);

            if (entity == null)
                return BaseResponse<BaseRegistration>.NotFound($"Registration with Id: {request.Id} was not found");

            entity = new Registration
            {
                Id = entity.Id,
                Title = request.Title,
                Content = request.Content,
                CreatedAt = entity.CreatedAt,
                LastUpdatedAt = DateTime.UtcNow,
                CategoryId = entity.CategoryId
            };

            var registration = await _registrationRepository.Update(entity);

            var detailedRegistration = GenericMapper<Registration, BaseRegistration>.Map(registration);
            var result = BaseResponse<BaseRegistration>.Ok(detailedRegistration);
            return result;
        }
        catch (Exception exception)
        {
            var result = BaseResponse<BaseRegistration>.BadRequest(new List<string>{exception.Message});
            return result;
        }
    }
}