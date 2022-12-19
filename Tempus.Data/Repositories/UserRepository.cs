using Tempus.Core.Entities;
using Tempus.Core.Repositories;
using Tempus.Data.Context;

namespace Tempus.Data.Repositories.UserRepositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(TempusDbContext context) : base(context)
    {
    }
}