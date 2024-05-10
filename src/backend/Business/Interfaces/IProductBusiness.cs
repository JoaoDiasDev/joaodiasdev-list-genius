using JoaoDiasDev.ListGenius.Data.VO;
using JoaoDiasDev.ListGenius.Hypermedia.Utils;

namespace JoaoDiasDev.ListGenius.Business.Interfaces
{
    public interface IProductBusiness
    {
        ProductVO Create(ProductVO person);
        ProductVO FindById(long id);
        List<ProductVO> FindByName(string name);
        List<ProductVO> FindAll();
        PagedSearchVO<ProductVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
        ProductVO Update(ProductVO person);
        ProductVO Disable(long id);
        void Delete(long id);
    }
}
