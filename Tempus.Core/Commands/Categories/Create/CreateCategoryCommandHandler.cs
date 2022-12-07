using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Models.Category;
using Tempus.Core.Repositories;

namespace Tempus.Core.Commands.Categories.Create;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, BaseResponse<BaseCategory>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUserRepository _userRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUserRepository userRepository)
    {
        _categoryRepository = categoryRepository;
        _userRepository = userRepository;
    }

    public async Task<BaseResponse<BaseCategory>> Handle(CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userRepository.GetById(request.UserId);
            if (user == null) return BaseResponse<BaseCategory>.BadRequest($"User with Id: {request.UserId} not found");

            var entity = new Category(Guid.NewGuid(), request.Name, DateTime.UtcNow, DateTime.UtcNow, request.Color,
                request.UserId);
            var category = await _categoryRepository.Add(entity);

            var result =
                BaseResponse<BaseCategory>.Ok(new BaseCategory(category.Id, category.Name, category.CreatedAt,
                    category.Color, category.UserId));
            return result;
        }
        catch (Exception exception)
        {
            return BaseResponse<BaseCategory>.BadRequest(exception.Message);
        }
    }
}