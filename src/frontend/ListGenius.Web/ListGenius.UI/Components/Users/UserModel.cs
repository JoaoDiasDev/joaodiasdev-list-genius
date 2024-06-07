namespace ListGenius.UI.Components.Users;

public class UserModel
{
    [Required(ErrorMessage = "Informe o email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a senha")]
    public string Password { get; set; } = string.Empty;
}
