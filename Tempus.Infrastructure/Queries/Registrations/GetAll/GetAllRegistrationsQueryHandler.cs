using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Core.Models.Registrations;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Queries.Registrations.GetAll;

public class
    GetAllRegistrationsQueryHandler : IRequestHandler<GetAllRegistrationsQuery,
        BaseResponse<List<RegistrationOverview>>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IRegistrationRepository _registrationRepository;

    public GetAllRegistrationsQueryHandler(IRegistrationRepository registrationRepository,
        ICategoryRepository categoryRepository)
    {
        _registrationRepository = registrationRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<BaseResponse<List<RegistrationOverview>>> Handle(GetAllRegistrationsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            List<Registration> registrations;
            if(request.CategoryId.HasValue)
            {
                registrations = await _registrationRepository.GetAll(request.CategoryId.Value, request.UserId);
            }
            else
            {
                registrations = await _registrationRepository.GetAll(request.UserId);
            }

            var registrationsOverview = registrations
                .Select(x =>
                {
                    var categoryColor = _categoryRepository.GetCategoryColor(x.CategoryId);

                    var currentRegistration = GenericMapper<Registration, RegistrationOverview>.Map(x);
                    currentRegistration.CategoryColor = categoryColor;
                    
                    return currentRegistration;
                })
                .ToList();
            
            var response = BaseResponse<List<RegistrationOverview>>.Ok(registrationsOverview);
            return response;
        }
        catch(Exception exception)
        {
            var response = BaseResponse<List<RegistrationOverview>>.BadRequest(new List<string> {exception.Message});
            return response;
        }
    }
}