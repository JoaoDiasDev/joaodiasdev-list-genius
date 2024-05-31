namespace ListGenius.Api.Entities.Users
{
    public class UserProfile
    {
        [JsonPropertyName("full_name")]
        [Display(Name = "Nome Completo")]
        public string FullName { get; set; } = string.Empty;
        [JsonPropertyName("email")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        [Display(Name = "Senha")]
        public string Password { get; set; } = string.Empty;
        [JsonPropertyName("logo_image")]
        [Display(Name = "Logo")]
        public byte[] LogoImage { get; set; } = [];
        [JsonPropertyName("profile_picture")]
        [Display(Name = "Foto Perfil")]
        public byte[] ProfilePicture { get; set; } = [];
    }
}

