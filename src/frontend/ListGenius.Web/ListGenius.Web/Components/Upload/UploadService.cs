namespace ListGenius.Web.Components.Upload;

public class UploadService : IUploadService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ILogger<UploadService> _logger;

    public UploadService(IHttpClientFactory httpClientFactory,
        ILogger<UploadService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<HttpResponseMessage> UploadFileAsync(string endpoint,
                                     MultipartFormDataContent content)
    {
        HttpResponseMessage responseMessage = null;
        try
        {
            var httpClient = _httpClientFactory.CreateClient("ApiMangas");

            responseMessage = await httpClient.PostAsync(endpoint, content);
            return responseMessage;
        }
        catch (Exception)
        {
            _logger.LogInformation($"Erro ao enviar o arquivo : {endpoint}");
            throw; //return responseMessage;
        }
    }
}
