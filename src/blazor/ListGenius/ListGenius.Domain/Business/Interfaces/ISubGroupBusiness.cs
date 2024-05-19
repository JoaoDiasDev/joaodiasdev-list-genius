using ListGenius.Shared.VO.PagedSearch;
using ListGenius.Shared.VO.SubGroup;

namespace ListGenius.Domain.Business.Interfaces
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
