using Tempus.Core.Commons;
using Tempus.Core.Models.Category;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Queries.Categories.GetAll;

public class GetAllCategoriesQuery : BaseRequest<BaseResponse<List<BaseCategory>>> { }