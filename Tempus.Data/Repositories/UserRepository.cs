using Tempus.Core.Entities;
using Tempus.Core.Repositories;
using Tempus.Data.Context;

namespace Tempus.Data.Repositories.UserRepositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly TempusDbContext _context;

    public UserRepository(TempusDbContext context) : base(context)
    {
    }
}