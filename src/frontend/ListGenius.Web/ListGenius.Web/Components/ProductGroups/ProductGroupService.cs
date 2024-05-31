namespace ListGenius.Web.Components.ProductGroups;

public class ProductGroupService(
    IHttpClientFactory httpClientFactory,
    ILogger<ProductGroupService> logger,
    IEnumerable<ProductGroupDto>? productGroups,
    ProductGroupDto? productGroup)
    : IProductGroupService
{
    private const string ApiEndpoint = "/api/productgroup/";

    public async Task<ProductGroupDto> GetById(int id)
    {
        try
        {
            var httpClient = httpClientFactory.CreateClient("ApiMangas");
            var response = await httpClient.GetAsync(ApiEndpoint + id);

            if (response.IsSuccessStatusCode)
            {
                productGroup = await response.Content.ReadFromJsonAsync<ProductGroupDto>();
                return productGroup ?? new ProductGroupDto();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                logger.LogError($"Erro ao obter a Product Group pelo id= {id} - {message}");
                throw new Exception($"Status Code : {response.StatusCode} - {message}");
            }
        }
        catch (Exception ex)
        {
            logger.LogError($"Erro ao obter a Product Group pelo id={id} \n\n {ex.Message} ");
            throw new UnauthorizedAccessException();
        }
    }

    public async Task<List<ProductGroupDto>> GetAll()
    {
        try
        {
            var httpClient = httpClientFactory.CreateClient("ApiListGenius");
            var result = await httpClient.GetFromJsonAsync<List<ProductGroupDto>>(ApiEndpoint);
            return result ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError($"Erro ao acessar Product Group: {ApiEndpoint} " + ex.Message);
            throw new UnauthorizedAccessException();
        }
    }
}