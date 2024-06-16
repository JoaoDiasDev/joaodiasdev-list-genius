namespace ClientLibrary.Helpers;

public class CustomAuthenticationStateProvider(LocalStorageService localStorageService) : AuthenticationStateProvider
{
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var stringToken = await localStorageService.GetToken();
        if (string.IsNullOrEmpty(stringToken)) return await Task.FromResult(new AuthenticationState(_anonymous));

        var deserializeToken = Serializations.DeserializeJsonString<UserSessionDto>(stringToken);
        if (deserializeToken is null) return await Task.FromResult(new AuthenticationState(_anonymous));

        var getUserClaims = DecryptToken(deserializeToken.Token!);
        if (getUserClaims is null) return await Task.FromResult(new AuthenticationState(_anonymous));

        var claimsPrincipal = SetClaimsPrincipal(getUserClaims);
        return await Task.FromResult(new AuthenticationState(claimsPrincipal));
    }

    public async Task UpdateAuthenticationState(UserSessionDto userSession)
    {
        ClaimsPrincipal claimsPrincipal = new();
        if (userSession.Token is not null || userSession.RefreshToken is not null)
        {
            var serializeSession = Serializations.SerializeObj(userSession);
            await localStorageService.SetToken(serializeSession);
            var getUserClaims = DecryptToken(userSession.Token!);
            claimsPrincipal = SetClaimsPrincipal(getUserClaims);
        }
        else
        {
            await localStorageService.RemoveToken();
        }
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    private static ClaimsPrincipal SetClaimsPrincipal(CustomUserClaimsDto claims)
    {
        if (claims.Email is null) return new ClaimsPrincipal();
        var claimsList = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, claims.Id),
            new(ClaimTypes.Name, claims.Name),
            new (ClaimTypes.Email, claims.Email),
            new (ClaimTypes.Role, claims.Role)
        };
        return new ClaimsPrincipal(new ClaimsIdentity(
            claimsList, "JwtAuth"));
    }

    private static CustomUserClaimsDto DecryptToken(string jwtToken)
    {
        if (string.IsNullOrEmpty(jwtToken)) return new CustomUserClaimsDto();

        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwtToken);
        var userId = token.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier));
        var name = token.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Name));
        var email = token.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email));
        var role = token.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Role));
        return new CustomUserClaimsDto(userId!.Value,
            name!.Value,
            email!.Value,
            role!.Value);
    }
}