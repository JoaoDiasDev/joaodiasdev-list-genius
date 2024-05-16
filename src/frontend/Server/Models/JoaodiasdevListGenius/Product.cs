using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius
{
    [Table("products")]
    public partial class Product
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
        public decimal value { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string description { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string qrcode { get; set; }

        [Required]
        [ConcurrencyCheck]
        public byte[] image { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string link { get; set; }

        [ConcurrencyCheck]
        public bool enabled { get; set; }

        [Required]
        [ConcurrencyCheck]
        public DateTime created_date { get; set; }

        [Required]
        [ConcurrencyCheck]
        public DateTime updated_date { get; set; }

        [Column("unit")]
        [Required]
        [ConcurrencyCheck]
        public string unit1 { get; set; }

        [Required]
        [ConcurrencyCheck]
        public long id_products_list { get; set; }

        public ProductsList ProductsList { get; set; }

        [Required]
        [ConcurrencyCheck]
        public long id_groups { get; set; }

        public Group Group { get; set; }

        [Required]
        [ConcurrencyCheck]
        public long id_sub_groups { get; set; }

        public SubGroup SubGroup { get; set; }

    }
}