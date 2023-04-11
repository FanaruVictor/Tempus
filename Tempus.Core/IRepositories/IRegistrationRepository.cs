using Tempus.Core.Entities;

namespace Tempus.Core.IRepositories;

public interface IRegistrationRepository : IBaseRepository<Registration>
{
    Task<List<Registration>> GetAll(Guid userId);
    Task<Registration> GetLastUpdated();
    Task<List<Registration>> GetAllFromGroup(Guid groupId);
    Task<Registration> GetById(Guid id, Guid groupId);
}