using Tempus.Core.Entities;

namespace Tempus.Core.IRepositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<bool?> GetTheme(Guid id);
    Task<User?> GetByExternalId(string externalId);
}