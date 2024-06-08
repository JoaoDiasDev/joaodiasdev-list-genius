using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;

namespace ListGenius.Web.Components.Auth
{
    public class ApiAuthenticationStateProvider(ILocalStorageService localStorage) : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var savedToken = await localStorage.GetItemAsync<string>("authToken");
                var tokenExpiration = await localStorage.GetItemAsync<string>("tokenExpiration");

                if (string.IsNullOrWhiteSpace(savedToken) || TokenHasExpired(tokenExpiration ?? string.Empty))
                {
                    MarkUserAsLoggedOut();
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }

                var claims = ParseClaimsFromJwt(savedToken);
                var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
                return new AuthenticationState(authenticatedUser);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in {nameof(GetAuthenticationStateAsync)}: {ex.Message}");
                MarkUserAsLoggedOut();
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

        public async Task<bool> VerifyLoggedUser()
        {
            try
            {
                var savedToken = await localStorage.GetItemAsync<string>("authToken");
                var tokenExpiration = await localStorage.GetItemAsync<string>("tokenExpiration");

                if (string.IsNullOrWhiteSpace(savedToken) || TokenHasExpired(tokenExpiration ?? string.Empty))
                {
                    return false;
                }

                var claims = ParseClaimsFromJwt(savedToken);

                var email = claims
                    .FirstOrDefault(c =>
                        c.Type.Equals("unique_name", StringComparison.OrdinalIgnoreCase))?.Value;


                if (email is null)
                {
                    return false;
                }

                MarkUserAsAuthenticated(email);

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in {nameof(VerifyLoggedUser)}: {ex.Message}");
                return false;
            }
        }

        public void MarkUserAsAuthenticated(string email)
        {
            //var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[]
            //{
            //    new Claim(ClaimTypes.Name, email)
            //}, "apiauth"));

            //var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            //NotifyAuthenticationStateChanged(authState);
        }

        public void MarkUserAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }

        private bool TokenHasExpired(string tokenExpiration)
        {
            if (DateTime.TryParse(tokenExpiration, out var expirationDate))
            {
                return expirationDate < DateTime.UtcNow;
            }
            return true;
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            if (keyValuePairs == null) return claims;

            keyValuePairs.TryGetValue(ClaimTypes.Role, out var roles);

            if (roles != null)
            {
                if (roles.ToString()!.Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString()!);
                    if (parsedRoles != null)
                    {
                        claims.AddRange(parsedRoles.Select(parsedRole => new Claim(ClaimTypes.Role, parsedRole)));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()!));
                }
                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!)));

            return claims;
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
