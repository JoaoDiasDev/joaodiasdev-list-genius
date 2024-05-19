using ListGenius.Shared.VO.PagedSearch;
using ListGenius.Shared.VO.Product;

namespace ListGenius.Domain.Business.Interfaces
{
    public interface IProductBusiness
    {
        ProductVO Create(ProductVO product);
        ProductVO FindById(long id);
        List<ProductVO> FindByName(string name);
        List<ProductVO> FindAll();
        PagedSearchVO<ProductVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
        ProductVO Update(ProductVO product);
        ProductVO Disable(long id);
        void Delete(long id);
    }
}
