using Tempus.Core.Entities;

namespace Tempus.Core.IRepositories;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<List<Category>> GetAll(Guid userId);
    string GetCategoryColor(Guid id);
}