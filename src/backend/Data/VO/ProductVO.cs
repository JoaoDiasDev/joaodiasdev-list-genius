using JoaoDiasDev.ListGenius.Data.Enums;
using JoaoDiasDev.ListGenius.Hypermedia;
using JoaoDiasDev.ListGenius.Hypermedia.Abstract;
using System.Text.Json.Serialization;

namespace JoaoDiasDev.ListGenius.Data.VO
{
    public class ProductVO : ISupportsHyperMedia
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
        public string qrcode { get; set; } = string.Empty;

        [JsonPropertyName("image")]
        public string Image { get; set; } = string.Empty;

        [JsonPropertyName("link")]
        public string Link { get; set; } = string.Empty;

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; } = true;

        [JsonPropertyName("unit")]
        public UnitOfMeasurement Unit { get; set; } = UnitOfMeasurement.Meter;
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
