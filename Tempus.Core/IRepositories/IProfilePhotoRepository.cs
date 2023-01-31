using Tempus.Core.Entities;

namespace Tempus.Core.IRepositories;

public interface IProfilePhotoRepository : IBaseRepository<ProfilePhoto>
{
    Task<ProfilePhoto> GetByUserId(Guid id);
}