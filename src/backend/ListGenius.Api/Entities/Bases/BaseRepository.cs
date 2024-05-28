using ListGenius.Api.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ListGenius.Api.Entities.Bases;
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly AppDbContext _db;
    protected readonly DbSet<TEntity> DbSet;
    protected BaseRepository(AppDbContext db)
    {
        _db = db;
        DbSet = db.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(int? id)
    {
        var entity = await DbSet.FindAsync(id);
        return entity!;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = DbSet;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        DbSet.Add(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        var existingEntity = await _db.Set<TEntity>().FindAsync(entity.Id) ?? throw new DbUpdateConcurrencyException();
        _db.Entry(existingEntity).CurrentValues.SetValues(entity);

        if (_db.Entry(existingEntity).State is EntityState.Modified)
        {
            await _db.SaveChangesAsync();
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task RemoveAsync(int? id)
    {
        var entity = await DbSet.FindAsync(id);
        DbSet.Remove(entity!);
        await _db.SaveChangesAsync();
    }

    public void Dispose()
    {
        _db?.Dispose();
    }

    public async Task<TEntity> Disable(int id)
    {
        var entity = await DbSet.FindAsync(id);
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        entity.Enabled = false;
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<TEntity>> FindAllByName(string name)
    {
        var entities = await DbSet
         .Where(e => EF.Functions.Like(e.Name, $"%{name}%"))
         .ToListAsync();

        if (entities.Count == 0)
        {
            throw new ArgumentNullException(nameof(entities), $"No entities containing name '{name}' were found.");
        }

        return entities;
    }

    public async Task<TEntity> FindByName<TEntity>(string name) where TEntity : class
    {
        return await _db.Set<TEntity>().FirstOrDefaultAsync(e => EF.Property<string>(e, "Name") == name);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await DbSet.AnyAsync(e => e.Id == id);
    }
}
