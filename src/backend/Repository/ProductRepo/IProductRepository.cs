using JoaoDiasDev.ProductList.Model;
using JoaoDiasDev.ProductList.Repository.Generic;

namespace JoaoDiasDev.ProductList.Repository.ProductRepo
{
    public interface IProductRepository : IRepository<Product>
    {
        Product Disable(long id);
        List<Product> FindByName(string Name);
    }
}
