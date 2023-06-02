namespace Tempus.Core.IRepositories;

public interface IGroupPhotoRepository : IBaseRepository<GroupPhoto>
{
    Task<GroupPhoto> GetByGroupId(Guid id);
}