using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Models.Registrations;
using Tempus.Core.Repositories;

namespace Tempus.Core.Queries.Registrations.GetAll;

public class GetAllRegistrationsQueryHandler : IRequestHandler<GetAllRegistrationsQuery, BaseResponse<List<DetailedRegistration>>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IRegistrationRepository _registrationRepository;

    public GetAllRegistrationsQueryHandler(IRegistrationRepository registrationRepository,
        ICategoryRepository categoryRepository)
    {
        _registrationRepository = registrationRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<BaseResponse<List<DetailedRegistration>>> Handle(GetAllRegistrationsQuery request,
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

            var response = BaseResponse<List<DetailedRegistration>>.Ok(registrations
                .Select(x =>
                {
                    var categoryColor = _categoryRepository.GetCategoryColor(x.CategoryId);

                    return new DetailedRegistration()
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Content = x.Content,
                        LastUpdatedAt = x.LastUpdatedAt,
                        CategoryColor = categoryColor,
                        CreatedAt = x.CreatedAt
                    };
                })
                .ToList());
            return response;
        }
        catch (Exception exception)
        {
            var response = BaseResponse<List<DetailedRegistration>>.BadRequest(new List<string>{exception.Message});
            return response;
        }
    }
}