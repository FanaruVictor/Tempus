using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Models.Category;

namespace Tempus.Core.Queries.Categories.GetAll;

public class GetAllCategoriesQuery : IRequest<BaseResponse<List<BaseCategory>>>
{
    public Guid? UserId { get; init; }
}