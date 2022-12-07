using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Models.Registration;
using Tempus.Core.Repositories;

namespace Tempus.Core.Commands.Registrations.Create;

public class CreateRegistrationCommandHandler : IRequestHandler<CreateRegistrationCommand, BaseResponse<DetailedRegistration>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IRegistrationRepository _registrationRepository;

    public CreateRegistrationCommandHandler(IRegistrationRepository registrationRepository,
        ICategoryRepository categoryRepository)
    {
        _registrationRepository = registrationRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<BaseResponse<DetailedRegistration>> Handle(CreateRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var category = await _categoryRepository.GetById(request.CategoryId);
            if (category == null)
                return BaseResponse<DetailedRegistration>.BadRequest($"Category with Id: {request.CategoryId} not found");

            var entity = new Registration(Guid.NewGuid(), request.Title, request.Content, DateTime.UtcNow, DateTime.UtcNow,
                category.Id);

            var registration = await _registrationRepository.Add(entity);

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