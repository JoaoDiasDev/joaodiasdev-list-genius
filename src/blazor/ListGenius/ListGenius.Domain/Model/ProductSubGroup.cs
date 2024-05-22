using ListGenius.Domain.Model.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ListGenius.Domain.Model
{
    [Table("product_sub_groups")]
    public class ProductSubGroup : BaseEntity
    {
        [Required]
        [Column("name")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Column("image", TypeName = "varbinary(max)")]
        public byte[] Image { get; set; } = [];

        [Column("description")]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column("enabled")]
        [DefaultValue(true)]
        public bool Enabled { get; set; } = true;

        [Required]
        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required]
        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [Required]
        [ForeignKey("ProductGroup")]
        [Column("id_product_groups")]
        public long IdProductGroups { get; set; }

        [JsonIgnore]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual ProductGroup ProductGroup { get; set; } = new ProductGroup();

        public virtual ICollection<Product> Products { get; set; } = [];
    }
}
