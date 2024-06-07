namespace ListGenius.UI.Components.Users;

public class UserLoginResult
{
    public string Error { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string Expiration { get; set; } = string.Empty;
}
