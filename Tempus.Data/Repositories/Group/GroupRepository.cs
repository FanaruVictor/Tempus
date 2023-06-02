using Microsoft.EntityFrameworkCore;
using Tempus.Core.Entities.User;
using Tempus.Core.IRepositories;
using Tempus.Data.Context;

namespace Tempus.Data.Repositories.Group;

public class GroupRepository : BaseRepository<Core.Entities.Group.Group>, IGroupRepository
{
    public GroupRepository(TempusDbContext context) : base(context) { }
    public Task<List<Core.Entities.Group.Group>> GetAll(Guid userId)
    {
        return _context.GroupUsers.AsNoTracking()
            .Include(x => x.Group)
            .ThenInclude(x => x.GroupPhoto)
            .Where(x => x.UserId == userId)
            .Select(x => x.Group)
            .ToListAsync();
    }

    public async Task<List<string>> GetUsersPhoto(Guid groupId)
    {
        return await _context.GroupUsers.AsNoTracking()
            .Include(x => x.User)
            .ThenInclude(x => x.UserPhoto)
            .Where(x => x.GroupId == groupId)
            .Select(x => x.User.UserPhoto.Url)
            .ToListAsync();
    }

    public int GetUserCount(Guid groupId)
    {
        return _context.GroupUsers.AsNoTracking().Where(x => x.Group.Id == groupId)?.Count( )?? 0;
    }

    public async Task<User> GetGroupUser(Guid userId, Guid groupId)
    {
        return await _context.GroupUsers.AsNoTracking()
            .Include(x => x.User)
            .Where(x => x.GroupId == groupId && x.UserId == userId)
            .Select(x => x.User)
            .FirstOrDefaultAsync();
    }
   
    public async Task<Guid> DeleteGroupMember(Guid userId, Guid groupId)
    {
        var groupUser = await _context.GroupUsers.AsNoTracking().FirstOrDefaultAsync(x => x.GroupId == groupId && x.UserId == userId);
        _context.GroupUsers.Remove(groupUser);
        return groupUser.UserId;
    }

    public Task<List<Guid>> GetUsers(Guid id)
    {
        return _context.GroupUsers.AsNoTracking()
            .Where(x => x.GroupId == id)
            .Select(x => x.UserId)
            .ToListAsync();
    }

    public override Task<Core.Entities.Group.Group> GetById(Guid id)
    {
        return _context.Groups.AsNoTracking()
            .Include(x => x.GroupPhoto)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<User>> GetGroupMembers(Guid groupId)
    {
        return await _context.GroupUsers.AsNoTracking()
            .Where(x => x.GroupId == groupId)
            .Select(x => x.User).ToListAsync();
    }
}