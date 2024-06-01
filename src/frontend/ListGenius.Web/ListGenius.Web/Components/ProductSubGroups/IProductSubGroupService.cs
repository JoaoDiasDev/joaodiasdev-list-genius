using ListGenius.Web.Components.ProductGroups;

namespace ListGenius.Web.Components.ProductSubGroups;
public interface IProductSubGroupService
{
    Task<List<ProductGroupDto>> GetAll();
    Task<ProductGroupDto> GetById(int id);
}
