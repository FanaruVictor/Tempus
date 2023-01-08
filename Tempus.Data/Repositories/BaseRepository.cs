using Microsoft.EntityFrameworkCore;
using Tempus.Core.Entities;
using Tempus.Core.Repositories;
using Tempus.Data.Context;

namespace Tempus.Data.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly TempusDbContext _context;

    protected BaseRepository(TempusDbContext context)
    {
        _context = context;
        _context.ChangeTracker.Clear();
    }


    public async Task<List<TEntity>> GetAll()
    {
        return await _context
            .Set<TEntity>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<TEntity?> GetById(Guid id)
    {
        return await _context
            .Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<TEntity> Add(TEntity entity)
    {
        await _context
            .Set<TEntity>()
            .AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> Update(TEntity entity)
    {
        _context
            .Set<TEntity>()
            .Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<Guid> Delete(Guid id)
    {
        var entity = await GetById(id);

        if (entity == null)
        {
            return Guid.Empty;
        }
        
        _context
            .Set<TEntity>()
            .Remove(entity);
        await _context.SaveChangesAsync();

        return id;
    }
}