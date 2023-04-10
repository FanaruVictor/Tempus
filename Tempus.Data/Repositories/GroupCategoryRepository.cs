using Tempus.Core.Entities.Group;
using Tempus.Core.IRepositories;
using Tempus.Data.Context;

namespace Tempus.Data.Repositories;

public class GroupCategoryRepository : IGroupCategoryRepository
{
    private readonly TempusDbContext _context;

    public GroupCategoryRepository(TempusDbContext context)
    {
        _context = context;
    }
    public Task<List<GroupCategory>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<GroupCategory> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task Add(GroupCategory entity)
    {
        await _context.GroupCategories.AddAsync(entity);
    }

    public void Update(GroupCategory entity)
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
}