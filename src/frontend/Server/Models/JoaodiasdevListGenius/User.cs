using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius
{
    [Table("users")]
    public partial class User
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

        [ConcurrencyCheck]
        public string user_name { get; set; }

        [ConcurrencyCheck]
        public string password { get; set; }

        [Required]
        [ConcurrencyCheck]
        public byte[] image { get; set; }

        [ConcurrencyCheck]
        public string email { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string full_name { get; set; }

        [ConcurrencyCheck]
        public bool enabled { get; set; }

        [ConcurrencyCheck]
        public string refresh_token { get; set; }

        [ConcurrencyCheck]
        public DateTime? refresh_token_expiry_time { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string role { get; set; }

        public ICollection<ProductsList> ProductsLists { get; set; }

    }
}