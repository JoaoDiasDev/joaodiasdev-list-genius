﻿namespace ClientLibrary.Helpers;

public class GetHttpClient(IHttpClientFactory httpClientFactory,
    LocalStorageService localStorageDevice)
{
    private const string HeaderKey = "Authorization";

    public async Task<HttpClient> GetPrivateHttpClient()
    {
        var client = httpClientFactory.CreateClient("SystemApiClient");
        var stringToken = await localStorageDevice.GetToken();
        if (string.IsNullOrEmpty(stringToken)) return client;

        var userSession = Serializations.DeserializeJsonString<UserSessionDto>(stringToken);
        if (userSession is null) return client;

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
            userSession.Token);

        return client;
    }

    public HttpClient GetPublicHttpClient()
    {
        var client = httpClientFactory.CreateClient("SystemApiClient");
        client.DefaultRequestHeaders.Remove(HeaderKey);
        return client;
    }
}