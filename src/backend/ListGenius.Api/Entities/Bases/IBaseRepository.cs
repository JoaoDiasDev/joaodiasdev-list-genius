using System.Linq.Expressions;

namespace ListGenius.Api.Entities.Bases;

public interface IBaseRepository<T> : IDisposable where T : BaseEntity
{
    Task AddAsync(T entity);
    Task<IEnumerable<T>?> GetAllAsync(params Expression<Func<T, object>>[] includes);
    Task<T?> GetByIdAsync(int id, params Expression<Func<T?, object>>[] includes);
    Task<bool> UpdateAsync(T entity, params Expression<Func<T?, object>>[] includes);
    Task RemoveAsync(int? id);

    Task<IEnumerable<T>?> SearchAsync(Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes);
    Task<T> Disable(int id);
    Task<bool> ExistsAsync<T>(int id, params Expression<Func<T, object>>[] includes) where T : BaseEntity;

    Task<T?> FindByProperty<T>(string propertyName, string value,
        params Expression<Func<T?, object>>[] includes) where T : BaseEntity;

    Task<IEnumerable<T>?> FindAllByProperty<T>(string propertyName, string value,
        params Expression<Func<T, object>>[] includes) where T : BaseEntity;
}


