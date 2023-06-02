using Microsoft.EntityFrameworkCore;
using Tempus.Core.IRepositories;
using Tempus.Data.Context;

namespace Tempus.Data.Repositories.Group;

public class GroupPhotoRepository : BaseRepository<GroupPhoto>, IGroupPhotoRepository
{
    public GroupPhotoRepository(TempusDbContext context) : base(context) { }

    public Task<GroupPhoto> GetByGroupId(Guid id)
    {
        return _context.GroupPhotos.AsNoTracking()
            .FirstOrDefaultAsync(x => x.GroupId == id);
    }
}