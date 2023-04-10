using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Entities.Group;
using Tempus.Core.Entities.User;
using Tempus.Core.IRepositories;
using Tempus.Core.Models.Category;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Commands.UserCategory.Create;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, BaseResponse<BaseCategory>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserCategoryRepository _userCategoryRepository;
    private readonly IGroupRepository _groupRepository;
    private readonly IGroupCategoryRepository _groupCategoryRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUserRepository userRepository,
        IUserCategoryRepository userCategoryRepository, IGroupRepository groupRepository,
        IGroupCategoryRepository groupCategoryRepository)
    {
        _categoryRepository = categoryRepository;
        _userRepository = userRepository;
        _userCategoryRepository = userCategoryRepository;
        _groupRepository = groupRepository;
        _groupCategoryRepository = groupCategoryRepository;
    }

    public async Task<BaseResponse<BaseCategory>> Handle(CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entity = new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                Color = request.Color,
            };

            BaseResponse<BaseCategory> result;

            if(request.GroupId.HasValue)
            {
                result = await AddGroupCategory(request, entity);
                
            }
            else
            {
                result = await AddUserCategory(request, entity);
            }

            if(result.StatusCode != StatusCodes.Ok)
            {
                return result;
            }

            await _categoryRepository.Add(entity);

            await _categoryRepository.SaveChanges();

            var baseCategory = GenericMapper<Category, BaseCategory>.Map(entity);
            result =
                BaseResponse<BaseCategory>.Ok(baseCategory);

            return result;
        }
        catch(Exception exception)
        {
            return BaseResponse<BaseCategory>.BadRequest(new List<string> {exception.Message});
        }
    }

    private async Task<BaseResponse<BaseCategory>> AddGroupCategory(CreateCategoryCommand request, Category category)
    {
        if(!request.GroupId.HasValue)
        {
            return BaseResponse<BaseCategory>.BadRequest(new List<string>
            {
                "The request does not contain a GroupId value"
            });
        }
        
        var group = await _groupRepository.GetById(request.GroupId.Value);
        
        if(group == null)
        {
            return BaseResponse<BaseCategory>.BadRequest(new List<string>
            {
                $"Group with Id: {request.GroupId} not found"
            });
        }
        
        var groupCategory = new GroupCategory
        {
            GroupId = request.GroupId.Value,
            CategoryId = category.Id
        };
        
        await _groupCategoryRepository.Add(groupCategory);
        
        return BaseResponse<BaseCategory>.Ok();
    }

    private async Task<BaseResponse<BaseCategory>> AddUserCategory(CreateCategoryCommand request, Category category)
    {
        var user = await _userRepository.GetById(request.UserId);
        if(user == null)
        {
            return BaseResponse<BaseCategory>.BadRequest(new List<string>
                {$"User with Id: {request.UserId} not found"});
        }

        var userCategory = new Core.Entities.User.UserCategory
        {
            UserId = request.UserId,
            CategoryId = category.Id
        };
        
        await _userCategoryRepository.Add(userCategory);

        return BaseResponse<BaseCategory>.Ok();
    }
}