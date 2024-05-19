using ListGenius.Domain.Model;
using ListGenius.Domain.Repository.Generic;

namespace ListGenius.Domain.Repository.ProductsListRepo
{
    public interface IProductsListRepository : IRepository<ProductsList>
    {
        ProductsList Disable(long id);
        List<ProductsList> FindByName(string Name);
    }
}
