using ListGenius.Domain.Model;
using ListGenius.Domain.Repository.Generic;

namespace ListGenius.Domain.Repository.ProductRepo
{
    public interface IProductRepository : IRepository<Product>
    {
        Product Disable(long id);
        List<Product> FindByName(string Name);
    }
}
