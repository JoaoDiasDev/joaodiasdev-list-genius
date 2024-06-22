using BaseLibrary.Entities;

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

    public async Task<List<ManageUserDto>> GetUsers()
    {
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var result = await httpClient.GetFromJsonAsync<List<ManageUserDto>>($"{AuthUrl}/users");
        if (result is null) throw new ArgumentNullException(nameof(result));
        return result;
    }

    public async Task<GeneralResponse> UpdateUser(ManageUserDto user)
    {
        var httpClient = getHttpClient.GetPublicHttpClient();
        var result = await httpClient.PutAsJsonAsync($"{AuthUrl}/update-user", user);
        if (!result.IsSuccessStatusCode) return new GeneralResponse(false, " Error occurred");
        return (await result.Content.ReadFromJsonAsync<GeneralResponse>())!;
    }

    public async Task<List<SystemRole>> GetRoles()
    {
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var result = await httpClient.GetFromJsonAsync<List<SystemRole>>($"{AuthUrl}/roles");
        if (result is null) throw new ArgumentNullException(nameof(result));
        return result;
    }

    public async Task<GeneralResponse> DeleteUser(int id)
    {
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var result = await httpClient.DeleteAsync($"{AuthUrl}/delete-user/{id}");
        if (!result.IsSuccessStatusCode) return new GeneralResponse(false, " Error occured");
        return (await result.Content.ReadFromJsonAsync<GeneralResponse>())!;
    }
}