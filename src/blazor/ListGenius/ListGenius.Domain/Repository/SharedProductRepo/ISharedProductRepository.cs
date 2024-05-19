using ListGenius.Domain.Model;
using ListGenius.Domain.Repository.Generic;

namespace ListGenius.Domain.Repository.SharedProductRepo
{
    public interface ISharedProductRepository : IRepository<SharedProduct>
    {
        SharedProduct Disable(long id);
        List<SharedProduct> FindByName(string Name);
    }
}
