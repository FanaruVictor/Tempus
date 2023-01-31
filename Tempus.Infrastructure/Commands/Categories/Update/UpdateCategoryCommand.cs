using Tempus.Infrastructure.Commons;
using Tempus.Core.Commons;
using Tempus.Infrastructure.Models.Category;

namespace Tempus.Infrastructure.Commands.Categories.Update;

public class UpdateCategoryCommand : BaseRequest<BaseResponse<BaseCategory>>
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string? Color { get; init; }
}