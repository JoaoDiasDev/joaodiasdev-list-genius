using ListGenius.Domain.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListGenius.Domain.Model
{
    [Table("shared_products")]
    public class SharedProduct : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("value")]
        public decimal Value { get; set; } = decimal.Zero;

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("qrcode")]
        public string Qrcode { get; set; } = string.Empty;

        [Column("image")]
        public byte[] Image { get; set; } = [];

        [Column("link")]
        public string Link { get; set; } = string.Empty;

        [Column("enabled")]
        public bool Enabled { get; set; } = true;

        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [Column("unit")]
        public string Unit { get; set; } = string.Empty;


        [Column("id_groups")]
        public string IdGroups { get; set; } = string.Empty;

        [Column("id_sub_groups")]
        public string IdSubGroups { get; set; } = string.Empty;
    }
}
