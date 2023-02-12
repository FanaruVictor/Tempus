using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.Category;

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

            categories = await _categoryRepository.GetAll(request.UserId);

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