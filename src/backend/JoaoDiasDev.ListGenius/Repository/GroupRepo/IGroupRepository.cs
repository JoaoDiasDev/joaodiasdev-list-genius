using JoaoDiasDev.ListGenius.Model;
using JoaoDiasDev.ListGenius.Repository.Generic;

namespace JoaoDiasDev.ListGenius.Repository.GroupRepo
{
    public interface IGroupRepository : IRepository<Group>
    {
        Group Disable(long id);
        List<Group> FindByName(string Name);
    }
}
