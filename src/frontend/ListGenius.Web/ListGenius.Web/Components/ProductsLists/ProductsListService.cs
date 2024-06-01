namespace ListGenius.Web.Components.ProductsLists;

public class ProductsListService(
    IHttpClientFactory httpClientFactory,
    ILogger<ProductsListService> logger,
    IEnumerable<ProductsListDto>? productsLists,
    ProductsListDto? productsList)
    : IProductsListService
{
    private const string ApiEndpoint = "/api/ProductsList/";

    public async Task<ProductsListDto> GetById(int id)
    {
        try
        {
            var httpClient = httpClientFactory.CreateClient("ApiListGenius");
            var response = await httpClient.GetAsync(ApiEndpoint + id);

            if (response.IsSuccessStatusCode)
            {
                productsList = await response.Content.ReadFromJsonAsync<ProductsListDto>();
                return productsList ?? new ProductsListDto();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                logger.LogError($"Erro ao obter a Products List pelo id= {id} - {message}");
                throw new Exception($"Status Code : {response.StatusCode} - {message}");
            }
        }
        catch (Exception ex)
        {
            logger.LogError($"Erro ao obter a Products List pelo id={id} \n\n {ex.Message} ");
            throw new UnauthorizedAccessException();
        }
    }

    public async Task<List<ProductsListDto>> GetAll()
    {
        try
        {
            var httpClient = httpClientFactory.CreateClient("ApiListGenius");
            var result = await httpClient.GetFromJsonAsync<List<ProductsListDto>>(ApiEndpoint);
            return result ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError($"Erro ao acessar Products List: {ApiEndpoint} " + ex.Message);
            throw new UnauthorizedAccessException();
        }
    }
}