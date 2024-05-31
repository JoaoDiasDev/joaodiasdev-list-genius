namespace ListGenius.Api.Entities.Users;

public class UserToken
{
    [JsonPropertyName("token")]
    [Display(Name = "Token")]
    public string Token { get; set; } = string.Empty;
    [JsonPropertyName("expiration")]
    [Display(Name = "Validade")]
    public DateTime Expiration { get; set; } = DateTime.Now;
}
