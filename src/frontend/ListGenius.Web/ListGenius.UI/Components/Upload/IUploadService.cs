namespace ListGenius.UI.Components.Upload;

public interface IUploadService
{
    Task<HttpResponseMessage> UploadFileAsync(
             string endpoint, MultipartFormDataContent content);
}
