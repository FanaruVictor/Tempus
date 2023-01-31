using Tempus.Infrastructure.Commons;
using Tempus.Core.Commons;

namespace Tempus.Infrastructure.Commands.Categories.Delete;

public class DeleteCategoryCommand : BaseRequest<BaseResponse<Guid>>
{
    public Guid Id { get; init; }
}