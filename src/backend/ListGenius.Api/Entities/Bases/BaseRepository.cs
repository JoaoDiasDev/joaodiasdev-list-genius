using ListGenius.Api.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ListGenius.Api.Entities.Bases;
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly AppDbContext _db;
    private readonly DbSet<TEntity> _dbSet;
    protected BaseRepository(AppDbContext db)
    {
        _db = db;
        _dbSet = db.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking().Where(predicate);
        return await GetEntitiesWithIncludesAsync(query, includes);
    }

    public async Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet.Where(e => e.Id == id);
        return await GetEntityWithIncludesAsync(query, includes) ?? throw new InvalidOperationException($"No entity found with ID {id}.");
    }

    public async Task AddAsync(TEntity entity)
    {
        _dbSet.Add(entity);
        await _db.SaveChangesAsync();
    }

    public async Task RemoveAsync(int? id)
    {
        var entity = await _dbSet.FindAsync(id) ?? throw new InvalidOperationException($"No entity found with ID {id}.");
        _dbSet.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<TEntity> Disable(int id)
    {
        var entity = await _dbSet.FindAsync(id) ?? throw new InvalidOperationException($"No entity found with ID {id}.");
        entity.Enabled = false;
        await _db.SaveChangesAsync();
        return entity;
    }

    public void Dispose()
    {
        _db?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
    {
        var query = includes.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>(_dbSet, (current, include) => current.Include(include));

        return await query.ToListAsync();
    }
    public async Task<bool> UpdateAsync(TEntity entity, params Expression<Func<TEntity, object>>[] includes)
    {
        var existingEntity = await GetByIdAsync(entity.Id, includes) ?? throw new DbUpdateConcurrencyException();
        _db.Entry(existingEntity).CurrentValues.SetValues(entity);

        if (_db.Entry(existingEntity).State == EntityState.Modified)
        {
            await _db.SaveChangesAsync();
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<TEntity> FindByProperty<TEntity>(string propertyName, string value, params Expression<Func<TEntity, object>>[] includes) where TEntity : BaseEntity
    {
        IQueryable<TEntity> query = _db.Set<TEntity>().Where(e => EF.Property<string>(e, propertyName) == value);
        return await GetEntityWithIncludesAsync(query, includes) ?? throw new InvalidOperationException($"No entity found with the informed value {value} for the property {propertyName}.");
    }

    public async Task<IEnumerable<TEntity>> FindAllByProperty<TEntity>(string propertyName, string value, params Expression<Func<TEntity, object>>[] includes) where TEntity : BaseEntity
    {
        IQueryable<TEntity> query = _db.Set<TEntity>().Where(e => EF.Functions.Like(EF.Property<string>(e, propertyName), $"%{value}%"));
        return await GetEntitiesWithIncludesAsync(query, includes) ?? throw new InvalidOperationException($"No entity found with the informed value {value} for the property {propertyName}.");
    }

    public async Task<bool> ExistsAsync<TEntity>(int id, params Expression<Func<TEntity, object>>[] includes) where TEntity : BaseEntity
    {
        IQueryable<TEntity> query = _db.Set<TEntity>().Where(e => e.Id == id);
        return await GetEntityWithIncludesAsync(query, includes) != null;
    }

    private async Task<TEntity> GetEntityWithIncludesAsync<TEntity>(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes) where TEntity : BaseEntity
    {
        var res = await GetEntitiesWithIncludesAsync(query, includes);
        return res.FirstOrDefault()!;
    }

    private async Task<IEnumerable<TEntity>> GetEntitiesWithIncludesAsync<TEntity>(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes) where TEntity : BaseEntity
    {
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.ToListAsync();
    }
}
