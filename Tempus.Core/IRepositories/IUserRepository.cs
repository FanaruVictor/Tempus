using Tempus.Core.Entities;
using Tempus.Core.Models.User;

namespace Tempus.Core.IRepositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<bool?> GetTheme(Guid id);
    Task<User?> GetByExternalId(string externalId);
    Task<List<UserEmail>> GetUsersEmails();
}