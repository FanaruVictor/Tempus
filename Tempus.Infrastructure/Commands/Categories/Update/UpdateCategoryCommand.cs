using Tempus.Core.Commons;
using Tempus.Core.Models.Category;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Commands.Categories.Update;

public class UpdateCategoryCommand : BaseRequest<BaseResponse<BaseCategory>>
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string? Color { get; init; }
}