using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Core.Models.Category;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Queries.Categories.GetAll;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, BaseResponse<List<BaseCategory>>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<BaseResponse<List<BaseCategory>>> Handle(GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            List<Category> categories;

            if(request.GroupId.HasValue)
            {
                categories = await _categoryRepository.GetAllForGroup(request.GroupId.Value);
            }
            else
            {
                categories = await _categoryRepository.GetAllForUser(request.UserId);
            }

            var response =
                BaseResponse<List<BaseCategory>>.Ok(categories
                    .Select(GenericMapper<Category, BaseCategory>.Map).ToList());
            return response;
        }
        catch(Exception exception)
        {
            var response = BaseResponse<List<BaseCategory>>.BadRequest(new List<string> {exception.Message});
            return response;
        }
    }
}