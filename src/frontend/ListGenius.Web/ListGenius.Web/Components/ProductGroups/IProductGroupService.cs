namespace ListGenius.Web.Components.ProductGroups;
public interface IProductGroupService
{
    Task<List<ProductGroupDto>> GetCategorias();
    Task<ProductGroupDto> GetCategoria(int id);
}
