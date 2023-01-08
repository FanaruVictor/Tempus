using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Models.Registrations;
using Tempus.Core.Repositories;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Commands.Registrations.Create;

public class CreateRegistrationCommandHandler : IRequestHandler<CreateRegistrationCommand, BaseResponse<BaseRegistration>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IRegistrationRepository _registrationRepository;

    public CreateRegistrationCommandHandler(IRegistrationRepository registrationRepository,
        ICategoryRepository categoryRepository)
    {
        _registrationRepository = registrationRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<BaseResponse<BaseRegistration>> Handle(CreateRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            var category = await _categoryRepository.GetById(request.CategoryId);
            if (category == null)
                return BaseResponse<BaseRegistration>.BadRequest(new List<string>{$"Category with Id: {request.CategoryId} not found"});

            var entity = new Registration{
                Id = Guid.NewGuid(),
                Title = request.Title, 
                Content = request.Content,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                CategoryId = category.Id
            };

            var registration = await _registrationRepository.Add(entity);

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