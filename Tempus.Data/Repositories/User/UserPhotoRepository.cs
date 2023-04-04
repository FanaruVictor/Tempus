using Microsoft.EntityFrameworkCore;
using Tempus.Core.Entities;
using Tempus.Core.Entities.User;
using Tempus.Core.IRepositories;
using Tempus.Data.Context;

namespace Tempus.Data.Repositories;

public class UserPhotoRepository : BaseRepository<UserPhoto>, IUserPhotoRepository
{
    public UserPhotoRepository(TempusDbContext context) : base(context) { }

    public async Task<UserPhoto> GetByUserId(Guid id)
    {
        return await _context.UserPhotos
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == id);
    }
}