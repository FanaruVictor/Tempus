using Microsoft.EntityFrameworkCore;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Data.Context;

namespace Tempus.Data.Repositories;

public class RegistrationRepository : BaseRepository<Registration>, IRegistrationRepository
{
    public RegistrationRepository(TempusDbContext context) : base(context) { }

    public async Task<List<Registration?>> GetAll(Guid categoryId, Guid userId)
    {
        var result = await _context.Registrations
            .AsNoTracking()
            .Include(x => x.Category)
            .Where(x => x.CategoryId == categoryId && x.Category.UserId == userId)
            .ToListAsync();

        return result;
    }

    public async Task<List<Registration>> GetAll(Guid userId)
    {
        var result = await _context.Registrations
            .AsNoTracking()
            .Include(x => x.Category)
            .Where(x => x.Category.UserId == userId)
            .ToListAsync();
        
        return result;
    }

    public Task<Registration?> GetLastUpdated()
    {
        return _context.Registrations.AsNoTracking().OrderByDescending(x => x.LastUpdatedAt).FirstOrDefaultAsync();
    }

    public override async Task<Registration> GetById(Guid id)
    {
        return await _context.Registrations
            .AsNoTracking()
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}