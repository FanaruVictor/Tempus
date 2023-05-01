using Microsoft.EntityFrameworkCore;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Data.Context;

namespace Tempus.Data.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    private const string DefaultColor = "#ffff";

    public CategoryRepository(TempusDbContext context) : base(context) { }

    public async Task<List<Category?>> GetAllForUser(Guid userId)
    {
        return await _context.UserCategories.AsNoTracking()
            .Where(x => x.UserId == userId)
            .Select(x => x.Category)
            .ToListAsync();
    }

    public async Task<List<Category?>> GetAllForGroup(Guid groupId)
    {
        return await _context.GroupCategories.AsNoTracking()
            .Where(x => x.GroupId == groupId)
            .Select(x => x.Category)
            .ToListAsync();
    }

    
    public string GetCategoryColor(Guid id)
    {
        return _context.Categories.AsNoTracking().Where(x => x.Id == id).Select(x => x.Color).FirstOrDefault() ??
               DefaultColor;
    }

    public override async Task<Category> GetById(Guid id)
    {
        return await _context.Categories
            .AsNoTracking()
            .Include(x => x.GroupCategories)
            .ThenInclude(x => x.Group)
            .ThenInclude(x => x.GroupUsers)
            .ThenInclude(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}