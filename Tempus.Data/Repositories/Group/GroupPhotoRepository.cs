using Tempus.Core.IRepositories;
using Tempus.Data.Context;

namespace Tempus.Data.Repositories.Group;

public class GroupPhotoRepository : BaseRepository<GroupPhoto>, IGroupPhotoRepository
{
    public GroupPhotoRepository(TempusDbContext context) : base(context) { }
    
}