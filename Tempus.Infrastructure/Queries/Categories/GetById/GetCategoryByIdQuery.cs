using Tempus.Infrastructure.Commons;
using Tempus.Core.Commons;
using Tempus.Infrastructure.Models.Category;

namespace Tempus.Infrastructure.Queries.Categories.GetById;

public class GetCategoryByIdQuery : BaseRequest<BaseResponse<BaseCategory>>
{
    public Guid Id { get; init; }
}