using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Models.Registration;
using Tempus.Core.Repositories;

namespace Tempus.Core.Queries.Registrations.GetAll;

public class GetAllRegistrationsQueryHandler : IRequestHandler<GetAllRegistrationsQuery, BaseResponse<List<RegistrationInfo>>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IRegistrationRepository _registrationRepository;

    public GetAllRegistrationsQueryHandler(IRegistrationRepository registrationRepository,
        ICategoryRepository categoryRepository)
    {
        _registrationRepository = registrationRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<BaseResponse<List<RegistrationInfo>>> Handle(GetAllRegistrationsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            List<Registration> registrations;
            if (request.CategoryId.HasValue)
                registrations = await _registrationRepository.GetAll(request.CategoryId.Value);
            else
                registrations = await _registrationRepository.GetAll();

            var response = BaseResponse<List<RegistrationInfo>>.Ok(registrations
                .Select(x =>
                {
                    var categoryColor = _categoryRepository.GetCategoryColor(x.CategoryId);

                    return new RegistrationInfo
                    {
                        Id = x.Id,
                        Title = x.Title,
                        LastUpdatedAt = x.LastUpdatedAt,
                        CategoryColor = categoryColor
                    };
                })
                .ToList());
            return response;
        }
        catch (Exception exception)
        {
            var response = BaseResponse<List<RegistrationInfo>>.BadRequest(exception.Message);
            return response;
        }
    }
}