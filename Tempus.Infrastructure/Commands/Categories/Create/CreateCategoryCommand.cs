using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.Category;

namespace Tempus.Infrastructure.Commands.Categories.Create;

public class CreateCategoryCommand : IRequest<BaseResponse<BaseCategory>>
{
    public Guid UserId { get; init; }
    public string Name { get; init; }
    public string? Color { get; init; }
}