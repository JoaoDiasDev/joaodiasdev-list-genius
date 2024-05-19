using ListGenius.Domain.Model;
using ListGenius.Domain.Repository.Generic;

namespace ListGenius.Domain.Repository.GroupRepo
{
    public interface IGroupRepository : IRepository<Group>
    {
        Group Disable(long id);
        List<Group> FindByName(string Name);
    }
}
