namespace ClientLibrary.Helpers;

public class CustomHttpHandler(LocalStorageService localStorageService,
    IUserAccountService userAccountService) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var loginUrl = request.RequestUri!.AbsoluteUri.Contains("login");
        var registerUrl = request.RequestUri!.AbsoluteUri.Contains("register");
        var refreshTokenUrl = request.RequestUri!.AbsoluteUri.Contains("refresh-token");


        if (loginUrl || registerUrl || refreshTokenUrl)
        {
            return await base.SendAsync(request, cancellationToken);
        }

        var result = await base.SendAsync(request, cancellationToken);
        if (result.StatusCode == HttpStatusCode.Unauthorized)
        {
            // Get token from localStorage
            var stringToken = await localStorageService.GetToken();
            if (stringToken == null) return result;

            // Check if the header contain token
            var token = string.Empty;
            try
            {
                token = request.Headers.Authorization!.Parameter!;
            }
            catch
            {
                //Ignored
            }

            var deserializedToken = Serializations.DeserializeJsonString<UserSessionDto>(stringToken);
            if (deserializedToken is null) return result;

            if (string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", deserializedToken.Token);
                return await base.SendAsync(request, cancellationToken);
            }

            // Call for refresh token
            var newJwtToken = await GetRefreshToken(deserializedToken.RefreshToken!);
            if (string.IsNullOrEmpty(newJwtToken)) return result;

            request.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", newJwtToken);
            return await base.SendAsync(request, cancellationToken);

        }
        return result;
    }

    private async Task<string> GetRefreshToken(string refreshToken)
    {
        var result = await userAccountService.RefreshTokenAsync(new RefreshTokenDto() { RefreshToken = refreshToken });
        var serializedToken = Serializations.SerializeObj(new UserSessionDto()
        {
            Token = result.Token,
            RefreshToken = result.RefreshToken
        });
        await localStorageService.SetToken(serializedToken);
        return result.Token;
    }
}