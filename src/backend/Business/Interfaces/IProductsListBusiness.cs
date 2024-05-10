using JoaoDiasDev.ListGenius.Data.VO;
using JoaoDiasDev.ListGenius.Hypermedia.Utils;

namespace JoaoDiasDev.ListGenius.Business.Interfaces
{
    public interface IProductsListBusiness
    {
        ProductsListVO Create(ProductsListVO productsList);
        ProductsListVO FindByID(long id);
        List<ProductsListVO> FindByName(string name);
        List<ProductsListVO> FindAll();
        PagedSearchVO<ProductsListVO> FindWithPagedSearch(string title, string sortDirection, int pageSize, int page);
        ProductsListVO Update(ProductsListVO productsList);
        ProductsListVO Disable(long id);
        void Delete(long id);
    }
}
