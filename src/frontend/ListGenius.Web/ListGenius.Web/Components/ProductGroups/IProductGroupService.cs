namespace ListGenius.Web.Components.ProductGroups;
public interface IProductsListService
{
    Task<List<ProductGroupDto>> GetAll();
    Task<ProductGroupDto> GetById(int id);
}
