using ListGenius.Domain.Model;
using ListGenius.Domain.Repository.Generic;

namespace ListGenius.Domain.Repository.SubGroupRepo
{
    public interface ISubGroupRepository : IRepository<SubGroup>
    {
        SubGroup Disable(long id);
        List<SubGroup> FindByName(string Name);
    }
}
