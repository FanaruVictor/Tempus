using Microsoft.EntityFrameworkCore;
using Tempus.Core.Entities.Group;
using Tempus.Core.Entities.User;
using Tempus.Core.IRepositories;
using Tempus.Data.Context;

namespace Tempus.Data.Repositories.Group;

public class GroupUserRepository : IGroupUserRepository
{
    private readonly TempusDbContext _context;

    public GroupUserRepository(TempusDbContext context)
    {
        _context = context;
    }

    public Task<List<GroupUser>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<GroupUser> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task Add(GroupUser entity)
    {
        await _context.GroupUsers.AddAsync(entity);
    }

    public void Update(GroupUser entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChanges()
    {
        throw new NotImplementedException();
    }

    public async Task AddRange(List<GroupUser> entities)
    {
        await _context.GroupUsers.AddRangeAsync(entities);
    }

    public async Task<List<Guid>> GetAllUsersFromGroup(Guid groupId)
    {
        return await _context.GroupUsers.Where(x => x.GroupId == groupId).Select(x => x.UserId).ToListAsync();
    }
    
    public async Task RemoveRange(List<GroupUser> entities)
    {
        await Task.Run(() => _context.GroupUsers.RemoveRange(entities));
    }
}