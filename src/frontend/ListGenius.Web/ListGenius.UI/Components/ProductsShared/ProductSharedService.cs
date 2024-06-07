using System.Text.Json;

namespace ListGenius.UI.Components.ProductsShared;

public class ProductSharedService(IHttpClientFactory httpClientFactory, ILogger<ProductSharedService> logger)
    : IProductSharedService
{
    private readonly string _baseUrl = "api/v1/ProductShared";
    private readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public async Task<IEnumerable<ProductSharedDto>> GetAllProductSharedAsync()
    {
        try
        {
            var httpClient = httpClientFactory.CreateClient("ApiListGenius");
            return await httpClient.GetFromJsonAsync<IEnumerable<ProductSharedDto>>($"{_baseUrl}") ?? Array.Empty<ProductSharedDto>();
        }
        catch (Exception e)
        {
            logger.LogError($"Erro ao obter produtos: {e.Message}");
            throw;
        }
    }

    public async Task<ProductSharedDto> GetProductSharedByIdAsync(int id)
    {
        try
        {
            var httpClient = httpClientFactory.CreateClient("ApiListGenius");
            return await httpClient.GetFromJsonAsync<ProductSharedDto>($"{_baseUrl}/{id}") ?? new ProductSharedDto();
        }
        catch (Exception e)
        {
            logger.LogError($"Erro ao obter produto com o id {id}: {e.Message}");
            throw;
        }
    }

    public async Task AddProductSharedAsync(ProductSharedDto product)
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

    public async Task UpdateProductSharedAsync(int id, ProductSharedDto product)
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

    public async Task DeleteProductSharedAsync(int id)
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