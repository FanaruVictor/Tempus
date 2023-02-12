using Tempus.Core.Commons;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Commands.Categories.Delete;

public class DeleteCategoryCommand : BaseRequest<BaseResponse<Guid>>
{
    public Guid Id { get; init; }
}