using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.Category;

namespace Tempus.Core.Queries.Categories.GetById;

public class GetCategoryByIdQuery : IRequest<BaseResponse<BaseCategory>>
{
    public Guid Id { get; init; }
}