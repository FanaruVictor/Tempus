using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.Category;

namespace Tempus.Infrastructure.Commands.Categories.Create;

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
            if(user == null)
            {
                return BaseResponse<BaseCategory>.BadRequest(new List<string>
                    {$"User with Id: {request.UserId} not found"});
            }

            var entity = new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                Color = request.Color,
                UserId = request.UserId
            };

            await _categoryRepository.Add(entity);
            await _categoryRepository.SaveChanges();

            var baseCategory = GenericMapper<Category, BaseCategory>.Map(entity);
            var result =
                BaseResponse<BaseCategory>.Ok(baseCategory);

            return result;
        }
        catch(Exception exception)
        {
            return BaseResponse<BaseCategory>.BadRequest(new List<string> {exception.Message});
        }
    }
}