using Tempus.Core.Entities;

namespace Tempus.Core.Repositories;

public interface IRegistrationRepository : IBaseRepository<Registration>
{
    Task<List<Registration>> GetAll(Guid categoryId);
    Task<Registration> GetLastUpdated();
}