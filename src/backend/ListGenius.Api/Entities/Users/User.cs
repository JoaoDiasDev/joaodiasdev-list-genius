namespace ListGenius.Api.Entities.Users;

public class User
{
    [JsonPropertyName("email")]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;
    [JsonPropertyName("password")]
    [Display(Name = "Senha")]
    public string Password { get; set; } = string.Empty;
}
