namespace ListGenius.Web.Components.ProductsLists;

public class ProductsListService(HttpClient httpClient) : IProductsListService
{
    private readonly string _baseUrl = "api/v1/ProductsList";

    public async Task<IEnumerable<ProductsListDto>> GetAllProductsListsAsync()
    {
        return await httpClient.GetFromJsonAsync<IEnumerable<ProductsListDto>>($"{_baseUrl}");
    }

    public async Task<ProductsListDto> GetProductsListByIdAsync(int id)
    {
        return await httpClient.GetFromJsonAsync<ProductsListDto>($"{_baseUrl}/{id}");
    }

    public async Task AddProductsListAsync(ProductsListDto productsList)
    {
        await httpClient.PostAsJsonAsync($"{_baseUrl}", productsList);
    }

    public async Task UpdateProductsListAsync(int id, ProductsListDto productsList)
    {
        await httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", productsList);
    }

    public async Task DeleteProductsListAsync(int id)
    {
        await httpClient.DeleteAsync($"{_baseUrl}/{id}");
    }
}