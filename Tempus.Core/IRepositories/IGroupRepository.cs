using Tempus.Core.Entities;
using Tempus.Core.Entities.Group;

namespace Tempus.Core.IRepositories;

public interface IGroupRepository : IBaseRepository<Group>
{
    Task<List<Group>> GetAll(Guid userId);
    Task<List<string>> GetUsersImages(Guid groupId);
}