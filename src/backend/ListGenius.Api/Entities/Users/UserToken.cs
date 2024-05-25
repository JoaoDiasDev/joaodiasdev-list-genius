namespace ListGenius.Api.Entities.Users;

public class UserToken
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
}
