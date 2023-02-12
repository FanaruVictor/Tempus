using Tempus.Core.Entities;

namespace Tempus.Core.IRepositories;

public interface IAuthRepository
{
    Task Register(User user, string password);
    Task<User> Login(string username, string password);
    Task<bool> UserExists(string username);
    Task<int> SaveChanges();
}