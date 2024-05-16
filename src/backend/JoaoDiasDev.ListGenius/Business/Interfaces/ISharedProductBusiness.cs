using JoaoDiasDev.ListGenius.Data.VO;
using JoaoDiasDev.ListGenius.Hypermedia.Utils;

namespace JoaoDiasDev.ListGenius.Business.Interfaces
{
    public interface ISharedProductBusiness
    {
        SharedProductVO Create(SharedProductVO sharedProduct);
        SharedProductVO FindById(long id);
        List<SharedProductVO> FindByName(string name);
        List<SharedProductVO> FindAll();
        PagedSearchVO<SharedProductVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
        SharedProductVO Update(SharedProductVO sharedProduct);
        SharedProductVO Disable(long id);
        void Delete(long id);
    }
}
