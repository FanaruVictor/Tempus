using System.Net.Sockets;
using Tempus.Core.Entities;

namespace Tempus.Core.Repositories;

public interface IUserRepository
{
    Task Add(User user);
    List<User> GetAll();
}