using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Models.Category;
using Tempus.Core.Repositories;

namespace Tempus.Core.Commands.Categories.Update;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, BaseResponse<BaseCategory>>
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<BaseResponse<BaseCategory>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await _categoryRepository.GetById(request.Id);

            if (entity == null)
                return BaseResponse<BaseCategory>.NotFound(
                    $"Category with Id: {request.Id} not found.");

            entity = new Category(entity.Id, request.Name, entity.CreatedAt, DateTime.UtcNow, request.Color,
                entity.UserId);

            var category = await _categoryRepository.Update(entity);

            var result = BaseResponse<BaseCategory>.Ok(new BaseCategory(category.Id, category.Name,
                category.LastUpdatedAt,
                category.Color, category.UserId));
            return result;
        }
        catch (Exception exception)
        {
            var result = BaseResponse<BaseCategory>.BadRequest(exception.Message);
            return result;
        }
    }
}