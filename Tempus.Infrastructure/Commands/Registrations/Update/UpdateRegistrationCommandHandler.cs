using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Core.Models.Registrations;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Commands.Registrations.Update;

public class
    UpdateRegistrationCommandHandler : IRequestHandler<UpdateRegistrationCommand, BaseResponse<RegistrationDetails>>
{
    private readonly IRegistrationRepository _registrationRepository;

    public UpdateRegistrationCommandHandler(IRegistrationRepository registrationRepository)
    {
        _registrationRepository = registrationRepository;
    }

    public async Task<BaseResponse<RegistrationDetails>> Handle(UpdateRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entity = await _registrationRepository.GetById(request.Id);

            if(entity == null)
            {
                return BaseResponse<RegistrationDetails>.NotFound($"Registration with Id: {request.Id} was not found");
            }

            if(entity.Category.UserId != request.UserId)
            {
                return BaseResponse<RegistrationDetails>.Forbbiden();
            }

            entity = new Registration
            {
                Id = entity.Id,
                Description = request.Description,
                Content = request.Content,
                CreatedAt = entity.CreatedAt,
                LastUpdatedAt = DateTime.UtcNow.Date,
                CategoryId = entity.CategoryId
            };

            await _registrationRepository.Update(entity);
            await _registrationRepository.SaveChanges();

            var detailedRegistration = GenericMapper<Registration, RegistrationDetails>.Map(entity);
            var result = BaseResponse<RegistrationDetails>.Ok(detailedRegistration);
            return result;
        }
        catch(Exception exception)
        {
            var result = BaseResponse<RegistrationDetails>.BadRequest(new List<string> {exception.Message});
            return result;
        }
    }
}