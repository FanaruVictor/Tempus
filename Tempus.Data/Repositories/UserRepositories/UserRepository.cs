using Tempus.Core.Entities;
using Tempus.Core.Repositories;
using Tempus.Data.Context;

namespace Tempus.Data.Repositories.UserRepositories;

public class UserRepository : IUserRepository
{
    private readonly TempusDbContext _context;

    public UserRepository(TempusDbContext context)
    {
        _context = context;
    }

    public async Task Add(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public List<User> GetAll()
    {
        return _context.Users.ToList();
    }
}