using JoaoDiasDev.ListGenius.Data.VO;
using JoaoDiasDev.ListGenius.Hypermedia.Utils;

namespace JoaoDiasDev.ListGenius.Business.Interfaces
{
    public interface ISubGroupBusiness
    {
        SubGroupVO Create(SubGroupVO subGroup);
        SubGroupVO FindById(long id);
        List<SubGroupVO> FindByName(string name);
        List<SubGroupVO> FindAll();
        PagedSearchVO<SubGroupVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
        SubGroupVO Update(SubGroupVO subGroup);
        SubGroupVO Disable(long id);
        void Delete(long id);
    }
}
