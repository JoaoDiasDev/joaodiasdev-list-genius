namespace ServerLibrary.Repositories.Contracts;

public interface IGenericRepository<T>
{
    Task<List<T>> GetAll();
    Task<T> GetById(int id);
    Task<GeneralResponse> Create(T item);
    Task<GeneralResponse> Update(T item);
    Task<GeneralResponse> DeleteById(int id);
}