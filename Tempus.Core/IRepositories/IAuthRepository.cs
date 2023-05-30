using Tempus.Core.Entities;
using Tempus.Core.Entities.User;

namespace Tempus.Core.IRepositories;

public interface IAuthRepository
{
    Task Register(User user);
    Task<User> Login(string email);
    Task<bool> IsEmailAlreadyRegistered(string email);
    Task<int> SaveChanges();
}