using JoaoDiasDev.ListGenius.Model;
using JoaoDiasDev.ListGenius.Repository.Generic;

namespace JoaoDiasDev.ListGenius.Repository.SharedProductRepo
{
    public interface ISharedProductRepository : IRepository<SharedProduct>
    {
        SharedProduct Disable(long id);
        List<SharedProduct> FindByName(string Name);
    }
}
