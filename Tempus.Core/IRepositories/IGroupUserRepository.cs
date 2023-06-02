using Tempus.Core.Entities;
using Tempus.Core.Entities.Group;

namespace Tempus.Core.IRepositories;

public interface IGroupUserRepository : IBaseRepository<GroupUser>
{
    Task AddRange(List<GroupUser> entities);
    Task<List<Guid>> GetAllUsersFromGroup(Guid GroupId);
    Task RemoveRange(List<GroupUser> entities);
}