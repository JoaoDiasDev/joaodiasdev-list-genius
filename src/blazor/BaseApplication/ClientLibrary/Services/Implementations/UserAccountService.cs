namespace ClientLibrary.Services.Implementations;

public class UserAccountService(GetHttpClient getHttpClient) : IUserAccountService
{
    private const string AuthUrl = "api/authentication";
    public async Task<GeneralResponse> CreateAsync(RegisterDto user)
    {
        var httpClient = getHttpClient.GetPublicHttpClient();
        var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/register", user);
        if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Error occurred");

        return (await result.Content.ReadFromJsonAsync<GeneralResponse>())!;
    }

    public async Task<LoginResponse> SignInAsync(LoginDto user)
    {
        var httpClient = getHttpClient.GetPublicHttpClient();
        var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/login", user);
        if (!result.IsSuccessStatusCode) return new LoginResponse(false, "Error occurred");

        return (await result.Content.ReadFromJsonAsync<LoginResponse>())!;
    }

    public async Task<LoginResponse> RefreshTokenAsync(RefreshTokenDto refreshToken)
    {
        var httpClient = getHttpClient.GetPublicHttpClient();
        var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/refresh-token", refreshToken);
        if (!result.IsSuccessStatusCode) return new LoginResponse(false, " Error occurred");

        return (await result.Content.ReadFromJsonAsync<LoginResponse>())!;
    }
}