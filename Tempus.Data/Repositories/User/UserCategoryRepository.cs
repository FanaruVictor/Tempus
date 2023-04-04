using Tempus.Core.Entities.User;
using Tempus.Core.IRepositories;
using Tempus.Data.Context;

namespace Tempus.Data.Repositories;

public class UserCategoryRepository : IUserCategoryRepository
{
    private readonly TempusDbContext _context;

    public UserCategoryRepository(TempusDbContext context)
    {
        _context = context;
    }
    
    public Task<List<UserCategory>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<UserCategory> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task Add(UserCategory entity)
    {
        await _context.UserCategories.AddAsync(entity);
    }

    public void Update(UserCategory entity)
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