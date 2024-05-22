using ListGenius.Domain.Context;
using ListGenius.Domain.Model.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ListGenius.Domain.Model
{
    [Table("products_list")]
    public class ProductsList : BaseEntity
    {
        [Required]
        [Column("name")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Column("description")]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column("enabled")]
        [DefaultValue(true)]
        public bool Enabled { get; set; } = true;

        [Column("image", TypeName = "varbinary(max)")]
        public byte[] Image { get; set; } = [];

        [Required]
        [Column("public_visibility")]
        [DefaultValue(false)]
        public bool PublicVisibility { get; set; } = false;

        [Required]
        [Column("total_products_count")]
        public int TotalProductsCount { get; set; } = default;

        [Required]
        [Column("total_products_value", TypeName = "decimal(18,2)")]
        public decimal TotalProductsValue { get; set; } = decimal.Zero;

        [Column("external_link")]
        [MaxLength(200)]
        public string ExternalLink { get; set; } = string.Empty;

        [Required]
        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required]
        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [Required]
        [ForeignKey("User")]
        [Column("id_user")]
        public string IdUser { get; set; } = string.Empty;

        public virtual ICollection<Product> Products { get; set; } = [];

        [JsonIgnore]
        public virtual ApplicationUser User { get; set; } = new ApplicationUser();
    }

}
