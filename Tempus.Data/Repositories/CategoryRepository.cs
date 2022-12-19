using Microsoft.EntityFrameworkCore;
using Tempus.Core.Entities;
using Tempus.Core.Repositories;
using Tempus.Data.Context;

namespace Tempus.Data.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    private const string DefaultColor = "#ffff";

    public CategoryRepository(TempusDbContext context) : base(context)
    {
    }

    public async Task<List<Category?>> GetAll(Guid userId)
    {
        return await _context.Categories.Where(x => x.UserId == userId).ToListAsync();
    }

    public string GetCategoryColor(Guid id)
    {
        return _context.Categories.Where(x => x.Id == id).Select(x => x.Color).FirstOrDefault() ?? DefaultColor;
    }
}