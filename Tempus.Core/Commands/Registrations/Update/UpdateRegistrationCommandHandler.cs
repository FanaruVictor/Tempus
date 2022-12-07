using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Models.Registration;
using Tempus.Core.Repositories;

namespace Tempus.Core.Commands.Registrations.Update;

public class UpdateRegistrationCommandHandler : IRequestHandler<UpdateRegistrationCommand, BaseResponse<DetailedRegistration>>
{
    private readonly IRegistrationRepository _registrationRepository;

    public UpdateRegistrationCommandHandler(IRegistrationRepository registrationRepository)
    {
        _registrationRepository = registrationRepository;
    }

    public async Task<BaseResponse<DetailedRegistration>> Handle(UpdateRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entity = await _registrationRepository.GetById(request.Id);

            if (entity == null)
                return BaseResponse<DetailedRegistration>.BadRequest($"Registration with Id: {request.Id} was not found");

            entity = new Registration(entity.Id, request.Title, request.Content, entity.CreatedAt, DateTime.UtcNow,
                entity.CategoryId);

            var registration = await _registrationRepository.Update(entity);

            var result = BaseResponse<DetailedRegistration>.Ok(new DetailedRegistration
            {
                Id = registration.Id,
                Title = registration.Title,
                Content = registration.Content
            });
            return result;
        }
        catch (Exception exception)
        {
            var result = BaseResponse<DetailedRegistration>.BadRequest(exception.Message);
            return result;
        }
    }
}