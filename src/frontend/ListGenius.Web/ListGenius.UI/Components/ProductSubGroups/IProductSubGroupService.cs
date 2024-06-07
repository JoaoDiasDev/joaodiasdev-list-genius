using ListGenius.UI.Components.ProductGroups;

namespace ListGenius.UI.Components.ProductSubGroups;
public interface IProductSubGroupService
{
    Task<List<ProductGroupDto>> GetAll();
    Task<ProductGroupDto> GetById(int id);
}
