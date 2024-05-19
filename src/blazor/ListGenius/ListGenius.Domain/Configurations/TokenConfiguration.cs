namespace ListGenius.Domain.Configurations
{
    public class TokenConfiguration
    {
        public string Audience { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
        public int MinutesUntilExpiration { get; set; } = 60;
        public int DaysUntilExpiration { get; set; } = 7;
    }
}
