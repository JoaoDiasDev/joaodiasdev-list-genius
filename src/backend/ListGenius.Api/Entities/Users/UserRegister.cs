
namespace ListGenius.Api.Entities.Users;

public class UserRegister
{
    [JsonPropertyName("full_name")]
    [Display(Name = "Nome Completo")]
    [Required]
    public string FullName { get; set; } = string.Empty;
    [JsonPropertyName("email")]
    [Display(Name = "Email")]
    [Required]
    public string Email { get; set; } = string.Empty;
    [JsonPropertyName("password")]
    [Display(Name = "Senha")]
    [Required]
    public string Password { get; set; } = string.Empty;
}
