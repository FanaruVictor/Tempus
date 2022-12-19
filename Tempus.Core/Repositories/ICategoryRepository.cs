using Tempus.Core.Entities;

namespace Tempus.Core.Repositories;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<List<Category>> GetAll(Guid userId);
    string GetCategoryColor(Guid id);
}