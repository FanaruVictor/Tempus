using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.Category;
using Tempus.Core.Repositories;

namespace Tempus.Core.Queries.Categories.GetById;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, BaseResponse<BaseCategory>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<BaseResponse<BaseCategory>> Handle(GetCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var category = await _categoryRepository.GetById(request.Id);

            if (category == null)
                return BaseResponse<BaseCategory>.NotFound(
                    "Category not found.");

            var response =
                BaseResponse<BaseCategory>.Ok(new BaseCategory(category.Id, category.Name, category.LastUpdatedAt,
                    category.Color, category.UserId));
            return response;
        }
        catch (Exception exception)
        {
            return BaseResponse<BaseCategory>.BadRequest(exception.Message);
        }
    }
}