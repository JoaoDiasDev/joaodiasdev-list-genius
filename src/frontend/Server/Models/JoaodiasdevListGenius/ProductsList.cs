using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius
{
    [Table("products_list")]
    public partial class ProductsList
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
        public string description { get; set; }

        [Required]
        [ConcurrencyCheck]
        public byte[] image { get; set; }

        [ConcurrencyCheck]
        public bool enabled { get; set; }

        [Column("public")]
        [ConcurrencyCheck]
        public bool public1 { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int total_products_count { get; set; }

        [Required]
        [ConcurrencyCheck]
        public decimal total_products_value { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string external_link { get; set; }

        [Required]
        [ConcurrencyCheck]
        public DateTime created_date { get; set; }

        [Required]
        [ConcurrencyCheck]
        public DateTime updated_date { get; set; }

        [Required]
        [ConcurrencyCheck]
        public long id_users { get; set; }

        public User User { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}