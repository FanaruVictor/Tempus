using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.Category;

namespace Tempus.Infrastructure.Commands.Categories.Update;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, BaseResponse<BaseCategory>>
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<BaseResponse<BaseCategory>> Handle(UpdateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entity = await _categoryRepository.GetById(request.Id);

            if(entity == null)
            {
                return BaseResponse<BaseCategory>.NotFound(
                    $"Category with Id: {request.Id} not found.");
            }

            entity = new Category
            {
                Id = entity.Id,
                Name = request.Name,
                CreatedAt = entity.CreatedAt,
                LastUpdatedAt = DateTime.UtcNow,
                Color = request.Color,
                UserId = entity.UserId
            };

            await _categoryRepository.Update(entity);
            await _categoryRepository.SaveChanges();

            var baseCategory = GenericMapper<Category, BaseCategory>.Map(entity);
            var result = BaseResponse<BaseCategory>.Ok(baseCategory);

            return result;
        }
        catch(Exception exception)
        {
            var result = BaseResponse<BaseCategory>.BadRequest(new List<string> {exception.Message});
            return result;
        }
    }
}