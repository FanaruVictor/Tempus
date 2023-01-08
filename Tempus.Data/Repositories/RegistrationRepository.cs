using Microsoft.EntityFrameworkCore;
using Tempus.Core.Entities;
using Tempus.Core.Repositories;
using Tempus.Data.Context;

namespace Tempus.Data.Repositories;

public class RegistrationRepository : BaseRepository<Registration>, IRegistrationRepository
{
    public RegistrationRepository(TempusDbContext context) : base(context)
    {
    }

    public async Task<List<Registration?>> GetAll(Guid categoryId) => await _context.Registrations.AsNoTracking().Where(x => x.CategoryId == categoryId).ToListAsync();

    public Task<Registration?> GetLastUpdated()
    {
        return _context.Registrations.AsNoTracking().OrderByDescending(x => x.LastUpdatedAt).FirstOrDefaultAsync();
    }
    
}