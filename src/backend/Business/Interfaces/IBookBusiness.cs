using JoaoDiasDev.ProductList.Data.VO;
using JoaoDiasDev.ProductList.Hypermedia.Utils;

namespace JoaoDiasDev.ProductList.Business.Interfaces
{
    public interface IBookBusiness
    {
        BookVO Create(BookVO book);
        BookVO FindByID(long id);
        PagedSearchVO<BookVO> FindWithPagedSearch(string? title, string sortDirection, int pageSize, int page);
        BookVO Update(BookVO book);
        void Delete(long id);
    }
}
