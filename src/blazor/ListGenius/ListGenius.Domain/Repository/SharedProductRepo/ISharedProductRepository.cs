using ListGenius.Domain.Model;
using ListGenius.Domain.Repository.Generic;

namespace ListGenius.Domain.Repository.SharedProductRepo
{
    public interface ISharedProductRepository : IRepository<ProductShared>
    {
        ProductShared Disable(long id);
        List<ProductShared> FindByName(string Name);
    }
}
