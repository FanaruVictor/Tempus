using Microsoft.EntityFrameworkCore;
using Tempus.Core.Entities;
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

    public async Task<User?> GetByExternalId(string externalId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.ExternalId == externalId);

        return user;
    }

    public override async Task<User> GetById(Guid id)
    {
        return await _context.Users
            .Include(x => x.ProfilePhoto)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}