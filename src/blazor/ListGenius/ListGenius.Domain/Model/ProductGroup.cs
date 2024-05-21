using ListGenius.Domain.Model.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListGenius.Domain.Model
{
    [Table("product_groups")]
    public class ProductGroup : BaseEntity
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
    }
}
