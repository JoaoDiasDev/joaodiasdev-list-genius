namespace ListGenius.UI.Components.ProductsLists;

public interface IProductsListService
{
    Task<IEnumerable<ProductsListDto>> GetAllProductsListsAsync();
    Task<ProductsListDto> GetProductsListByIdAsync(int id);
    Task AddProductsListAsync(ProductsListDto productsList);
    Task UpdateProductsListAsync(int id, ProductsListDto productsList);
    Task DeleteProductsListAsync(int id);
}