using Tempus.Core.Commons;
using Tempus.Core.Models.Category;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Queries.Categories.GetById;

public class GetCategoryByIdQuery : BaseRequest<BaseResponse<BaseCategory>>
{
    public Guid Id { get; init; }
}