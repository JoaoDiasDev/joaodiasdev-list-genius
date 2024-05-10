using JoaoDiasDev.ProductList.Hypermedia;
using JoaoDiasDev.ProductList.Hypermedia.Abstract;
using System.Text.Json.Serialization;

namespace JoaoDiasDev.ProductList.Data.VO
{
    public class PersonVO : ISupportsHyperMedia
    {
        [JsonPropertyName("code")]
        public long Id { get; set; }
        [JsonPropertyName("name")]
        public string FirstName { get; set; }
        [JsonPropertyName("last_name")]
        public string LastName { get; set; }
        [JsonIgnore]
        public string Address { get; set; }
        [JsonPropertyName("sex")]
        public string Gender { get; set; }
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
