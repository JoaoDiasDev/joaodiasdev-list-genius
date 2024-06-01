namespace ListGenius.Web.Components.ProductsLists;
public interface IProductsListService
{
    Task<List<ProductsListDto>> GetAll();
    Task<ProductsListDto> GetById(int id);
}
