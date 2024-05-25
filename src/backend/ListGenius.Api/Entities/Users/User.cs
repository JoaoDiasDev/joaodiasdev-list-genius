namespace ListGenius.Api.Entities.Users;

public class User
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public byte[] LogoImage { get; set; } = [];
    public byte[] ProfilePicture { get; set; } = [];
}
