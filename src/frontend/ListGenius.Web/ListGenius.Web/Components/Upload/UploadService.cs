namespace ListGenius.Web.Components.Upload;

public class UploadService(
    IHttpClientFactory httpClientFactory,
    ILogger<UploadService> logger)
    : IUploadService
{
    public async Task<HttpResponseMessage> UploadFileAsync(string endpoint,
                                     MultipartFormDataContent content)
    {
        try
        {
            var httpClient = httpClientFactory.CreateClient("ApiListGenius");

            var responseMessage = await httpClient.PostAsync(endpoint, content);
            return responseMessage;
        }
        catch (Exception)
        {
            logger.LogInformation($"Erro ao enviar o arquivo : {endpoint}");
            throw;
        }
    }
}
