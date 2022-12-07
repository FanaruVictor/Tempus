using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Models.Category;
using Tempus.Core.Repositories;

namespace Tempus.Core.Queries.Categories.GetAll;

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

            if (request.UserId.HasValue)
                categories = await _categoryRepository.GetAll(request.UserId.Value);
            else
                categories = await _categoryRepository.GetAll();

            var response =
                BaseResponse<List<BaseCategory>>.Ok(categories
                    .Select(x => new BaseCategory(x.Id, x.Name, x.LastUpdatedAt, x.Color, x.UserId)).ToList());
            return response;
        }
        catch (Exception exception)
        {
            var response = BaseResponse<List<BaseCategory>>.BadRequest(exception.Message);
            return response;
        }
    }
}