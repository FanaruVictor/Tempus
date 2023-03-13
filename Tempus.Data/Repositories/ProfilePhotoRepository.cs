using Microsoft.EntityFrameworkCore;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Data.Context;

namespace Tempus.Data.Repositories;

public class ProfilePhotoRepository : BaseRepository<ProfilePhoto>, IProfilePhotoRepository
{
    public ProfilePhotoRepository(TempusDbContext context) : base(context) { }

    public async Task<ProfilePhoto> GetByUserId(Guid id)
    {
        return await _context.ProfilePhotos
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == id);
    }
}