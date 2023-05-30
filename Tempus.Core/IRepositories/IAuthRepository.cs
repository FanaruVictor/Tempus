using Tempus.Core.Entities;
using Tempus.Core.Entities.User;

namespace Tempus.Core.IRepositories;

public interface IAuthRepository
{
    Task Register(User user, string password);
    Task<User> Login(string email, string password);
    Task<bool> IsEmailAlreadyRegistered(string email);
    Task<bool> IsUsernameAlreadyRegistered(string username);
    Task<int> SaveChanges();
}