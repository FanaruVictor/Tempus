using Tempus.Core.Entities;

namespace Tempus.Core.IRepositories;

public interface IRegistrationRepository : IBaseRepository<Registration>
{
    Task<List<Registration>> GetAll(Guid categoryId, Guid userId);
    Task<List<Registration>> GetAll(Guid userId);
    Task<Registration> GetLastUpdated();
}