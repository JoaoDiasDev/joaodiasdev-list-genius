using ListGenius.Domain.Model;
using ListGenius.Domain.Repository.Generic;

namespace ListGenius.Domain.Repository.SubGroupRepo
{
    public interface ISubGroupRepository : IRepository<ProductSubGroup>
    {
        ProductSubGroup Disable(long id);
        List<ProductSubGroup> FindByName(string Name);
    }
}
