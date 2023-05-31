using Tempus.Core.Entities;
using Tempus.Core.Entities.User;

namespace Tempus.Core.IRepositories;

public interface IAuthRepository
{
    Task Register(User user);
    Task<User> Login(string email, string externalId);
    Task<bool> IsEmailAlreadyRegistered(string email);
    Task<bool> IsExternalIdAlreadyRegistered(string externalId);
    Task<int> SaveChanges();
}