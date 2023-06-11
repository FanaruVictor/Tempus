using Microsoft.EntityFrameworkCore;
using Tempus.Core.Entities;
using Tempus.Core.Entities.User;
using Tempus.Core.IRepositories;
using Tempus.Data.Context;

namespace Tempus.Data.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(TempusDbContext context) : base(context) { }

    public async Task<bool?> GetTheme(Guid id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        return user?.IsDarkTheme;
    }
    
    public override async Task<List<User>> GetAll()
    {
        return await _context.Users.AsNoTracking()
            .Include(x => x.UserPhoto)
            
            .ToListAsync();
    }

    public override async Task<User> GetById(Guid id)
    {
        return await _context.Users
            .AsNoTracking()
            .Include(x => x.UserPhoto)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}