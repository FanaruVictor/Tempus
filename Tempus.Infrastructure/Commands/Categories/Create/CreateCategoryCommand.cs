using Tempus.Core.Commons;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.Category;

namespace Tempus.Infrastructure.Commands.Categories.Create;

public class CreateCategoryCommand : BaseRequest<BaseResponse<BaseCategory>>
{
    public string Name { get; init; }
    public string? Color { get; init; }
}