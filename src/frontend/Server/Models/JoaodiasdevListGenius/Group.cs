using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius
{
    [Table("groups")]
    public partial class Group
    {

        [NotMapped]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("@odata.etag")]
        public string ETag
        {
                get;
                set;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string name { get; set; }

        [Required]
        [ConcurrencyCheck]
        public byte[] image { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string description { get; set; }

        [ConcurrencyCheck]
        public bool enabled { get; set; }

        [Required]
        [ConcurrencyCheck]
        public DateTime created_date { get; set; }

        [Required]
        [ConcurrencyCheck]
        public DateTime updated_date { get; set; }

        public ICollection<Product> Products { get; set; }

        public ICollection<SharedProduct> SharedProducts { get; set; }

        public ICollection<SubGroup> SubGroups { get; set; }

    }
}