using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.Registrations;

namespace Tempus.Infrastructure.Commands.Registrations.Create;

public class
    CreateRegistrationCommandHandler : IRequestHandler<CreateRegistrationCommand, BaseResponse<RegistrationDetails>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IRegistrationRepository _registrationRepository;

    public CreateRegistrationCommandHandler(IRegistrationRepository registrationRepository,
        ICategoryRepository categoryRepository)
    {
        _registrationRepository = registrationRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<BaseResponse<RegistrationDetails>> Handle(CreateRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var category = await _categoryRepository.GetById(request.CategoryId);
            if(category == null)
            {
                return BaseResponse<RegistrationDetails>.BadRequest(new List<string>
                    {$"Category with Id: {request.CategoryId} not found"});
            }

            var entity = new Registration
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Content = request.Content,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                CategoryId = request.CategoryId,
                UserId = request.UserId,
            };

            await _registrationRepository.Add(entity);
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