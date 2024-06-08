using Blazored.LocalStorage;
using ListGenius.Web.Components.Users;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ListGenius.Web.Components.Auth
{
    public class AuthService(
        IHttpClientFactory httpClientFactory,
        AuthenticationStateProvider authenticationStateProvider,
        ILocalStorageService localStorage)
        : IAuthService
    {
        public async Task<UserLoginResult?> Login(UserModel loginModel)
        {
            try
            {
                var httpClient = httpClientFactory.CreateClient("ApiListGenius");
                var loginAsJson = JsonSerializer.Serialize(loginModel);
                var requestContent = new StringContent(loginAsJson, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("api/User/Login", requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var loginResult = JsonSerializer.Deserialize<UserLoginResult>(
                    await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (loginResult?.Token is null)
                {
                    return null;
                }

                await localStorage.SetItemAsync("authToken", loginResult.Token);
                await localStorage.SetItemAsync("tokenExpiration", loginResult.Expiration);

                ((ApiAuthenticationStateProvider)authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email);

                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", loginResult.Token);

                return loginResult;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task Logout()
        {
            var httpClient = httpClientFactory.CreateClient("ApiListGenius");
            await localStorage.RemoveItemAsync("authToken");
            await localStorage.RemoveItemAsync("tokenExpiration");

            ((ApiAuthenticationStateProvider)authenticationStateProvider).MarkUserAsLoggedOut();
            httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<UserProfile?> GetUserProfile()
        {
            var httpClient = httpClientFactory.CreateClient("ApiListGenius");

            var authState = await ((ApiAuthenticationStateProvider)authenticationStateProvider).GetAuthenticationStateAsync();
            var email = authState.User.FindFirst("unique_name")?.Value;

            var response = await httpClient.GetAsync($"api/User/UserProfile?email={email}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var userProfile = JsonSerializer.Deserialize<UserProfile>(
                await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (userProfile?.FullName is null)
            {
                return null;
            }

            return userProfile;

        }

        public async Task<AuthenticationState> GetAuthenticationState()
        {
            return await ((ApiAuthenticationStateProvider)authenticationStateProvider).GetAuthenticationStateAsync();
        }

        public async Task<bool> VerifyLoggedUser()
        {
            return await ((ApiAuthenticationStateProvider)authenticationStateProvider).VerifyLoggedUser();
        }

        public async Task<bool> UpdateLogoImage(UserUpdateImage userUpdateImage)
        {
            var httpClient = httpClientFactory.CreateClient("ApiListGenius");
            var loginAsJson = JsonSerializer.Serialize(userUpdateImage);
            var requestContent = new StringContent(loginAsJson, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("api/User/UpdateLogoImage", requestContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateProfileImage(UserUpdateImage userUpdateImage)
        {
            var httpClient = httpClientFactory.CreateClient("ApiListGenius");
            var loginAsJson = JsonSerializer.Serialize(userUpdateImage);
            var requestContent = new StringContent(loginAsJson, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("api/User/UpdateProfileImage", requestContent);

            return response.IsSuccessStatusCode;
        }
    }
}
