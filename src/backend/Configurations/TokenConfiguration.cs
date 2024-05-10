namespace JoaoDiasDev.ProductList.Configurations
{
    public class TokenConfiguration
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public int MinutesUntilExpiration { get; set; }
        public int DaysUntilExpiration { get; set; }
    }
}
