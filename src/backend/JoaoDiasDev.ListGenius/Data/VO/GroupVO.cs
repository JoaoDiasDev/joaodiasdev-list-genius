using JoaoDiasDev.ListGenius.Hypermedia;
using JoaoDiasDev.ListGenius.Hypermedia.Abstract;
using System.Text.Json.Serialization;

namespace JoaoDiasDev.ListGenius.Data.VO
{
    public class GroupVO : ISupportsHyperMedia
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("image")]
        public byte[] Image { get; set; } = [];

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; } = true;

        [JsonPropertyName("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [JsonPropertyName("updated_date")]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
