using Tempus.Core.Entities;
using Tempus.Core.Entities.Group;
using Tempus.Core.Entities.User;

namespace Tempus.Core.IRepositories;

public interface IGroupRepository : IBaseRepository<Group>
{
    Task<List<Group>> GetAll(Guid userId);
    Task<List<string>> GetUsersImages(Guid groupId); 
    int GetUserCount(Guid groupId);

    Task<User?> GetGroupUser(Guid userId, Guid groupId);
    Task<Guid> DeleteGroupMember(Guid userId, Guid groupId);
}