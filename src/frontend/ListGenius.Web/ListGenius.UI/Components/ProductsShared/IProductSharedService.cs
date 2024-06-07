namespace ListGenius.UI.Components.ProductsShared;

public interface IProductSharedService
{
    Task<IEnumerable<ProductSharedDto>> GetAllProductSharedAsync();
    Task<ProductSharedDto> GetProductSharedByIdAsync(int id);
    Task AddProductSharedAsync(ProductSharedDto productShared);
    Task UpdateProductSharedAsync(int id, ProductSharedDto productShared);
    Task DeleteProductSharedAsync(int id);
}
