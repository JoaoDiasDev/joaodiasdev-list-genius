using Blazored.LocalStorage;

namespace ListGenius.Web.Util;
public class CustomHttpHandler(ILocalStorageService localStorageService) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                       CancellationToken cancellationToken)
    {
        if (request.RequestUri is not null && (request.RequestUri.AbsolutePath.ToLower().Contains("login") ||
                                           request.RequestUri.AbsolutePath.ToLower().Contains("register")))
        {
            return await base.SendAsync(request, cancellationToken);
        }


        var jwtToken = await localStorageService.GetItemAsync<string>("authToken", cancellationToken);

        if (!string.IsNullOrEmpty(jwtToken))
        {
            request.Headers.Add("Authorization", $"bearer {jwtToken}");
        }
        return await base.SendAsync(request, cancellationToken);
    }
}