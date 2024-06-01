namespace ListGenius.Web.Components.Upload;

public interface IUploadService
{
    Task<HttpResponseMessage> UploadFileAsync(
             string endpoint, MultipartFormDataContent content);
}
