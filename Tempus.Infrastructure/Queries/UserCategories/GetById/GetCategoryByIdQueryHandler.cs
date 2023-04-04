using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Core.Models.Category;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Queries.Categories.GetById;

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

            if(category == null)
            {
                return BaseResponse<BaseCategory>.NotFound("Category not found.");
            }

            var baseCategory = GenericMapper<Category, BaseCategory>.Map(category);
            var response = BaseResponse<BaseCategory>.Ok(baseCategory);

            return response;
        }
        catch(Exception exception)
        {
            return BaseResponse<BaseCategory>.BadRequest(new List<string> {exception.Message});
        }
    }
}