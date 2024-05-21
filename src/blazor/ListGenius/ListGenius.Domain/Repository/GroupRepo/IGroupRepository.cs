using ListGenius.Domain.Model;
using ListGenius.Domain.Repository.Generic;

namespace ListGenius.Domain.Repository.GroupRepo
{
    public interface IGroupRepository : IRepository<ProductGroup>
    {
        ProductGroup Disable(long id);
        List<ProductGroup> FindByName(string Name);
    }
}
