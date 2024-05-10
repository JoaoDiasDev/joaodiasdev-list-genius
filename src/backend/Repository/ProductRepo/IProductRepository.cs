using JoaoDiasDev.ListGenius.Model;
using JoaoDiasDev.ListGenius.Repository.Generic;

namespace JoaoDiasDev.ListGenius.Repository.ProductRepo
{
    public interface IProductRepository : IRepository<Product>
    {
        Product Disable(long id);
        List<Product> FindByName(string Name);
    }
}
