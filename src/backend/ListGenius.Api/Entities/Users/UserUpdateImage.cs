namespace ListGenius.Api.Entities.Users
{
    public class UserUpdateImage
    {
        [JsonPropertyName("email")]
        public required string Email { get; set; }
        [JsonPropertyName("image_base_64")]
        public required string ImageBase64 { get; set; }
    }
}
