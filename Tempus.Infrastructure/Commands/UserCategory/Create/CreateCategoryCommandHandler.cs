using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Core.Models.Category;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Commands.UserCategory.Create;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, BaseResponse<BaseCategory>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserCategoryRepository _userCategoryRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUserRepository userRepository, IUserCategoryRepository userCategoryRepository)
    {
        _categoryRepository = categoryRepository;
        _userRepository = userRepository;
        _userCategoryRepository = userCategoryRepository;
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
            };
            await _categoryRepository.Add(entity);
            
            var userCategory = new Core.Entities.User.UserCategory
            {
                UserId = request.UserId,
                CategoryId = entity.Id
            };
            await _userCategoryRepository.Add(userCategory);
            
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