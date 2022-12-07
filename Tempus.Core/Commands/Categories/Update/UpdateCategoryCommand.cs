using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.Category;

namespace Tempus.Core.Commands.Categories.Update;

public class UpdateCategoryCommand : IRequest<BaseResponse<BaseCategory>>
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string? Color { get; init; }
}