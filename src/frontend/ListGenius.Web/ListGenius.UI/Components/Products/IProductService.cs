namespace ListGenius.UI.Components.Products;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<ProductDto> GetProductByIdAsync(int id);
    Task AddProductAsync(ProductDto product);
    Task UpdateProductAsync(int id, ProductDto product);
    Task DeleteProductAsync(int id);
}