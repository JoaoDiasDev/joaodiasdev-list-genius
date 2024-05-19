using ListGenius.Shared.VO.PagedSearch;
using ListGenius.Shared.VO.ProductList;

namespace ListGenius.Domain.Business.Interfaces
{
    public interface IProductsListBusiness
    {
        ProductsListVO Create(ProductsListVO productsList);
        ProductsListVO FindByID(long id);
        List<ProductsListVO> FindByName(string name);
        List<ProductsListVO> FindAll();
        PagedSearchVO<ProductsListVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
        ProductsListVO Update(ProductsListVO productsList);
        ProductsListVO Disable(long id);
        void Delete(long id);
    }
}
