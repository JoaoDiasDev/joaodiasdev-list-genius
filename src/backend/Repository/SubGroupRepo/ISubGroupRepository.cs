using JoaoDiasDev.ListGenius.Model;
using JoaoDiasDev.ListGenius.Repository.Generic;

namespace JoaoDiasDev.ListGenius.Repository.SubGroupRepo
{
    public interface ISubGroupRepository : IRepository<SubGroup>
    {
        SubGroup Disable(long id);
        List<SubGroup> FindByName(string Name);
    }
}
