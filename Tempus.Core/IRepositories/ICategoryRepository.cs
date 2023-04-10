using Tempus.Core.Entities;

namespace Tempus.Core.IRepositories;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<List<Category>> GetAllForUser(Guid userId);
    Task<List<Category>> GetAllForGroup(Guid userId);
    string GetCategoryColor(Guid id);
}