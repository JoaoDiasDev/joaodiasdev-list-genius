using JoaoDiasDev.ListGenius.Hypermedia;
using JoaoDiasDev.ListGenius.Hypermedia.Abstract;
using System.Text.Json.Serialization;

namespace JoaoDiasDev.ListGenius.Data.VO
{
    public class SharedProductVO : ISupportsHyperMedia
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("value")]
        public decimal Value { get; set; } = decimal.Zero;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("qrcode")]
        public string Qrcode { get; set; } = string.Empty;

        [JsonPropertyName("image")]
        public byte[] Image { get; set; } = [];

        [JsonPropertyName("link")]
        public string Link { get; set; } = string.Empty;

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; } = true;

        [JsonPropertyName("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [JsonPropertyName("updated_date")]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [JsonPropertyName("unit")]
        public string Unit { get; set; } = string.Empty;

        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
