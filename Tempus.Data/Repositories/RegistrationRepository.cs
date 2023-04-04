using Microsoft.EntityFrameworkCore;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Data.Context;

namespace Tempus.Data.Repositories;

public class RegistrationRepository : BaseRepository<Registration>, IRegistrationRepository
{
    public RegistrationRepository(TempusDbContext context) : base(context) { }

    public async Task<List<Registration>> GetAll(Guid userId)
    {
        var result = await _context.Registrations
            .AsNoTracking()
            .Include(x => x.Category)
            .Where(x => x.Category.UserCategories.Any(y => y.UserId == userId))
            .ToListAsync();
        
        return result;
    }

    public Task<Registration?> GetLastUpdated()
    {
        return _context.Registrations.AsNoTracking().OrderByDescending(x => x.LastUpdatedAt).FirstOrDefaultAsync();
    }

    public Task<List<Registration>> GetAllFromGroup(Guid groupId)
    {
        return _context.Registrations
            .AsNoTracking()
            .Include(x => x.Category)
            .ThenInclude(x => x.GroupCategories)
            .Where(x => x.Category.GroupCategories.Any(y => y.GroupId == groupId))
            .ToListAsync();
    }

    public override async Task<Registration> GetById(Guid id)
    {
        return await _context.Registrations
            .AsNoTracking()
            .Include(x => x.Category)
            .ThenInclude(x => x.UserCategories)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}