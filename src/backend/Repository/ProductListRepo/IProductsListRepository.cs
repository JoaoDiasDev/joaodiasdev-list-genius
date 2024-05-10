using JoaoDiasDev.ListGenius.Model;
using JoaoDiasDev.ListGenius.Repository.Generic;

namespace JoaoDiasDev.ListGenius.Repository.ProductListRepo
{
    public interface IProductsListRepository : IRepository<ProductsList>
    {
        ProductsList Disable(long id);
        List<ProductsList> FindByName(string Name);
    }
}
