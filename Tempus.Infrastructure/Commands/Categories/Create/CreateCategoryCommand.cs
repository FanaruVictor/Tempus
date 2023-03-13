using Tempus.Core.Commons;
using Tempus.Core.Models.Category;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Commands.Categories.Create;

public class CreateCategoryCommand : BaseRequest<BaseResponse<BaseCategory>>
{
    public string Name { get; init; }
    public string? Color { get; init; }
}