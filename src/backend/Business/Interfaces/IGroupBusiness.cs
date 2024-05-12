using JoaoDiasDev.ListGenius.Data.VO;
using JoaoDiasDev.ListGenius.Hypermedia.Utils;

namespace JoaoDiasDev.ListGenius.Business.Interfaces
{
    public interface IGroupBusiness
    {
        GroupVO Create(GroupVO group);
        GroupVO FindById(long id);
        List<GroupVO> FindByName(string name);
        List<GroupVO> FindAll();
        PagedSearchVO<GroupVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
        GroupVO Update(GroupVO group);
        GroupVO Disable(long id);
        void Delete(long id);
    }
}
