using Microsoft.EntityFrameworkCore;
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

    public async Task<List<string>> GetUsersImages(Guid groupId)
    {
        return await _context.GroupUsers.AsNoTracking()
            .Include(x => x.User)
            .ThenInclude(x => x.UserPhoto)
            .Where(x => x.GroupId == groupId)
            .Select(x => x.User.UserPhoto.Url)
            .ToListAsync();
    }
}