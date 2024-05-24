using ListGenius.Domain.Model.Base;

namespace ListGenius.Domain.Repository.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {

        T Create(T item);

        bool Delete(long id);

        bool Exists(long id);

        List<T> FindAll();

        T FindById(long id);

        T Update(T item);
        List<T> FindWithPagedSearch(string query);
        int GetCount(string query);
    }
}
