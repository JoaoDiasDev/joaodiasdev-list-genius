using ListGenius.Api.Extensions.Cache;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace ListGenius.Api.Entities.Bases;
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly AppDbContext _db;
    private readonly DbSet<TEntity> _dbSet;
    private readonly IMemoryCache _cache;
    private readonly MemoryCacheEntryOptions _cacheOptions;

    protected BaseRepository(AppDbContext db, IMemoryCache cache)
    {
        _db = db;
        _dbSet = db.Set<TEntity>();
        _cache = cache;
        _cacheOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(30));
    }

    public async Task<IEnumerable<TEntity>?> SearchAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        var cacheKey = $"SearchAsync_{predicate}_{string.Join(",", includes.Select(i => i.ToString()))}";
        if (!_cache.TryGetValue(cacheKey, out IEnumerable<TEntity>? cachedEntities))
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking().Where(predicate);
            cachedEntities = await GetEntitiesWithIncludesAsync(query, includes);
            _cache.Set(cacheKey, cachedEntities, _cacheOptions);
        }
        return cachedEntities;
    }

    public async Task<TEntity?> GetByIdAsync(int id, params Expression<Func<TEntity?, object>>[] includes)
    {
        var cacheKey = $"GetByIdAsync_{id}_{string.Join(",", includes.Select(i => i.ToString()))}";
        if (!_cache.TryGetValue(cacheKey, out TEntity? cachedEntity))
        {
            IQueryable<TEntity> query = _dbSet.Where(e => e.Id == id);
            cachedEntity = await GetEntityWithIncludesAsync(query, includes) ?? throw new InvalidOperationException($"No entity found with ID {id}.");
            _cache.Set(cacheKey, cachedEntity, _cacheOptions);
        }
        return cachedEntity;
    }

    public async Task AddAsync(TEntity entity)
    {
        _dbSet.Add(entity);
        await _db.SaveChangesAsync();
        await ClearCacheAsync();
    }

    public async Task RemoveAsync(int? id)
    {
        var entity = await _dbSet.FindAsync(id) ?? throw new InvalidOperationException($"No entity found with ID {id}.");
        _dbSet.Remove(entity);
        await _db.SaveChangesAsync();
        await ClearCacheAsync();
    }

    public async Task<TEntity> Disable(int id)
    {
        var entity = await _dbSet.FindAsync(id) ?? throw new InvalidOperationException($"No entity found with ID {id}.");
        entity.Enabled = false;
        await _db.SaveChangesAsync();
        await ClearCacheAsync();
        return entity;
    }

    public void Dispose()
    {
        _db?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<IEnumerable<TEntity>?> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
    {
        var cacheKey = $"GetAllAsync_{string.Join(",", includes.Select(i => i.ToString()))}";
        if (!_cache.TryGetValue(cacheKey, out IEnumerable<TEntity>? cachedEntities))
        {
            var query = includes.Aggregate(_dbSet.AsNoTracking(), (current, include) => current.Include(include));
            cachedEntities = await query.ToListAsync();
            _cache.Set(cacheKey, cachedEntities, _cacheOptions);
        }
        return cachedEntities;
    }

    public async Task<bool> UpdateAsync(TEntity entity, params Expression<Func<TEntity?, object>>[] includes)
    {
        var existingEntity = await GetByIdAsync(entity.Id, includes) ?? throw new DbUpdateConcurrencyException();
        _db.Entry(existingEntity).CurrentValues.SetValues(entity);

        if (_db.Entry(existingEntity).State == EntityState.Modified)
        {
            await _db.SaveChangesAsync();
            await ClearCacheAsync();
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<TEntity?> FindByProperty<TEntity>(string propertyName, string value, params Expression<Func<TEntity?, object>>[] includes) where TEntity : BaseEntity
    {
        var cacheKey = $"FindByProperty_{propertyName}_{value}_{string.Join(",", includes.Select(i => i.ToString()))}";
        if (!_cache.TryGetValue(cacheKey, out TEntity? cachedEntity))
        {
            IQueryable<TEntity?> query = _db.Set<TEntity>().Where(e => EF.Property<string>(e, propertyName) == value);
            cachedEntity = await GetEntityWithIncludesAsync(query, includes) ?? throw new InvalidOperationException($"No entity found with the informed value {value} for the property {propertyName}.");
            _cache.Set(cacheKey, cachedEntity, _cacheOptions);
        }
        return cachedEntity;
    }

    public async Task<IEnumerable<TEntity>?> FindAllByProperty<TEntity>(string propertyName, string value, params Expression<Func<TEntity, object>>[] includes) where TEntity : BaseEntity
    {
        var cacheKey = $"FindAllByProperty_{propertyName}_{value}_{string.Join(",", includes.Select(i => i.ToString()))}";
        if (!_cache.TryGetValue(cacheKey, out IEnumerable<TEntity>? cachedEntities))
        {
            IQueryable<TEntity> query = _db.Set<TEntity>().Where(e => EF.Functions.Like(EF.Property<string>(e, propertyName), $"%{value}%"));
            cachedEntities = await GetEntitiesWithIncludesAsync(query, includes) ?? throw new InvalidOperationException($"No entity found with the informed value {value} for the property {propertyName}.");
            _cache.Set(cacheKey, cachedEntities, _cacheOptions);
        }
        return cachedEntities;
    }

    public async Task<bool> ExistsAsync<TEntity>(int id, params Expression<Func<TEntity, object>>[] includes) where TEntity : BaseEntity
    {
        var cacheKey = $"ExistsAsync_{id}_{string.Join(",", includes.Select(i => i.ToString()))}";
        if (!_cache.TryGetValue(cacheKey, out bool exists))
        {
            IQueryable<TEntity> query = _db.Set<TEntity>().Where(e => e.Id == id);
            exists = await GetEntityWithIncludesAsync(query, includes) != null;
            _cache.Set(cacheKey, exists, _cacheOptions);
        }
        return exists;
    }

    private async Task<TEntity?> GetEntityWithIncludesAsync<TEntity>(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes) where TEntity : BaseEntity?
    {
#pragma warning disable CS8631 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match constraint type.
        var res = await GetEntitiesWithIncludesAsync(query, includes);
#pragma warning restore CS8631 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match constraint type.
        return res.FirstOrDefault();
    }

    private async Task<IEnumerable<TEntity>> GetEntitiesWithIncludesAsync<TEntity>(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes) where TEntity : BaseEntity
    {
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.ToListAsync();
    }

    private async Task ClearCacheAsync()
    {
        var keys = _cache.GetKeys<string>();
        var tasks = keys.Select(key => Task.Run(() => _cache.Remove(key)));
        await Task.WhenAll(tasks);
    }
}
