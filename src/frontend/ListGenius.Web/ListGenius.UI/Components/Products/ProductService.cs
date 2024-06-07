using System.Text.Json;

namespace ListGenius.UI.Components.Products;

public class ProductService(IHttpClientFactory httpClientFactory, ILogger<ProductService> logger)
    : IProductService
{
    private readonly string _baseUrl = "api/v1/Product";
    private readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        try
        {
            var httpClient = httpClientFactory.CreateClient("ApiListGenius");
            return await httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>($"{_baseUrl}") ?? Array.Empty<ProductDto>();
        }
        catch (Exception e)
        {
            logger.LogError($"Erro ao obter produtos: {e.Message}");
            throw;
        }
    }

    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        try
        {
            var httpClient = httpClientFactory.CreateClient("ApiListGenius");
            return await httpClient.GetFromJsonAsync<ProductDto>($"{_baseUrl}/{id}") ?? new ProductDto();
        }
        catch (Exception e)
        {
            logger.LogError($"Erro ao obter produto com o id {id}: {e.Message}");
            throw;
        }
    }

    public async Task AddProductAsync(ProductDto product)
    {
        try
        {
            var httpClient = httpClientFactory.CreateClient("ApiListGenius");
            await httpClient.PostAsJsonAsync($"{_baseUrl}", product);
        }
        catch (Exception e)
        {
            logger.LogError($"Erro ao criar o produto {product.Name}: {e.Message}");
            throw;
        }
    }

    public async Task UpdateProductAsync(int id, ProductDto product)
    {
        try
        {
            var httpClient = httpClientFactory.CreateClient("ApiListGenius");
            await httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", product);
        }
        catch (Exception e)
        {
            logger.LogError($"Erro ao atualizar o produto {product.Name}: {e.Message}");
            throw;
        }
    }

    public async Task DeleteProductAsync(int id)
    {
        try
        {
            var httpClient = httpClientFactory.CreateClient("ApiListGenius");
            await httpClient.DeleteAsync($"{_baseUrl}/{id}");
        }
        catch (Exception e)
        {
            logger.LogError($"Erro ao deletar o produto com id {id}: {e.Message}");
            throw;
        }
    }
}