using Microsoft.EntityFrameworkCore;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Data.Context;

namespace Tempus.Data.Repositories.UserRepositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(TempusDbContext context) : base(context)
    {
    }

    public async Task<bool?> GetTheme(Guid id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        return user?.IsDarkTheme;
    }
}