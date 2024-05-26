using System.Linq.Expressions;

namespace ListGenius.Api.Entities.Bases;

public interface IBaseRepository<T> : IDisposable where T : BaseEntity
{
    Task AddAsync(T entity);
    Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
    Task<T> GetByIdAsync(int? id);
    Task UpdateAsync(T entity);
    Task RemoveAsync(int? id);
    Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate);
    Task<T> Disable(int id);
    Task<IEnumerable<T>> FindByName(string name);
}
