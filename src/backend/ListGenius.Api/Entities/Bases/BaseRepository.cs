using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;
#pragma warning disable CS8631 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match constraint type.

namespace ListGenius.Api.Entities.Bases;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly AppDbContext _db;
    private readonly DbSet<TEntity> _dbSet;
    private readonly IMemoryCache _cache;

    protected BaseRepository(AppDbContext db, IMemoryCache cache)
    {
        _db = db;
        _dbSet = db.Set<TEntity>();
        _cache = cache;
    }

    public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking().Where(predicate);
        return await GetEntitiesWithIncludesAsync(query, includes);
    }

    public async Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes)
    {
        var cacheKey = $"{typeof(TEntity).Name}_{id}";
        if (!_cache.TryGetValue(cacheKey, out TEntity? entity))
        {
            IQueryable<TEntity?> query = _dbSet.Where(e => e.Id == id);
            entity = await GetEntityWithIncludesAsync(query, includes!) ?? throw new InvalidOperationException($"No entity found with ID {id}.");
            var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
            _cache.Set(cacheKey, entity, cacheOptions);
        }
        return entity ?? throw new InvalidOperationException($"No Entity found with ID {id}.");
    }

    public async Task AddAsync(TEntity entity)
    {
        _dbSet.Add(entity);
        await _db.SaveChangesAsync();
        InvalidateCache();
    }

    public async Task RemoveAsync(int? id)
    {
        var entity = await _dbSet.FindAsync(id) ?? throw new InvalidOperationException($"No entity found with ID {id}.");
        _dbSet.Remove(entity);
        await _db.SaveChangesAsync();
        InvalidateCache();
    }

    public async Task<TEntity> Disable(int id)
    {
        var entity = await _dbSet.FindAsync(id) ?? throw new InvalidOperationException($"No entity found with ID {id}.");
        entity.Enabled = false;
        await _db.SaveChangesAsync();
        InvalidateCache();
        return entity;
    }

    public void Dispose()
    {
        _db?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
    {
        var cacheKey = $"{typeof(TEntity).Name}_All";
        if (!_cache.TryGetValue(cacheKey, out IEnumerable<TEntity>? entities))
        {
            var query = includes.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>(_dbSet, (current, include) => current.Include(include));
            entities = await query.ToListAsync();
            var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
            _cache.Set(cacheKey, entities, cacheOptions);
        }
        return entities ?? throw new InvalidOperationException($"No Entities found.");
    }

    public async Task<bool> UpdateAsync(TEntity entity, params Expression<Func<TEntity, object>>[] includes)
    {
        var existingEntity = await GetByIdAsync(entity.Id, includes) ?? throw new DbUpdateConcurrencyException();
        _db.Entry(existingEntity).CurrentValues.SetValues(entity);

        if (_db.Entry(existingEntity).State == EntityState.Modified)
        {
            await _db.SaveChangesAsync();
            InvalidateCache();
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

    private void InvalidateCache()
    {
        _cache.Remove($"{typeof(TEntity).Name}_All");
        // Additional cache keys can be invalidated as necessary
    }
}
