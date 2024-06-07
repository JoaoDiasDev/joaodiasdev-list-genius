using Blazored.LocalStorage;
using ListGenius.UI.Components.Users;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ListGenius.UI.Components.Auth;

public class AuthService(
    IHttpClientFactory httpClientFactory,
    AuthenticationStateProvider authenticationStateProvider,
    ILocalStorageService localStorage)
    : IAuthService
{
    public async Task<UserLoginResult?> Login(UserModel loginModel)
    {
        var httpClient = httpClientFactory.CreateClient("ApiListGenius");
        var loginAsJson = JsonSerializer.Serialize(loginModel);
        var requestContent = new StringContent(loginAsJson, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("api/User/Login", requestContent);

        var loginResult = JsonSerializer.Deserialize<UserLoginResult>
        (await response.Content.ReadAsStringAsync(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        if (!response.IsSuccessStatusCode)
        {
            return loginResult;
        }

        await localStorage.SetItemAsync("authToken", loginResult?.Token);
        await localStorage.SetItemAsync("tokenExpiration", loginResult?.Expiration);

        ((ApiAuthenticationStateProvider)authenticationStateProvider)
            .MarkUserAsAuthenticated(loginModel.Email);

        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("bearer",
                loginResult?.Token);

        return loginResult;
    }

    public async Task Logout()
    {
        var httpClient = httpClientFactory.CreateClient("ApiListGenius");
        await localStorage.RemoveItemAsync("authToken");

        ((ApiAuthenticationStateProvider)authenticationStateProvider).MarkUserAsLoggedOut();
        httpClient.DefaultRequestHeaders.Authorization = null;
    }
}
