using Tempus.Core.Entities;
using Tempus.Core.Entities.User;

namespace Tempus.Core.IRepositories;

public interface IUserPhotoRepository : IBaseRepository<UserPhoto>
{
    Task<UserPhoto> GetByUserId(Guid id);
}