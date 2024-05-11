using JoaoDiasDev.ListGenius.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoaoDiasDev.ListGenius.Model
{
    [Table("products_list")]
    public class ProductsList : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("enabled")]
        public bool Enabled { get; set; } = true;

        [Column("image")]
        public byte[] Image { get; set; } = [];

        [Column("public")]
        public bool Public { get; set; } = false;

        [Column("total_products_count")]
        public int TotalProductsCount { get; set; } = default;

        [Column("total_products_value")]
        public decimal TotalProductsValue { get; set; } = decimal.Zero;

        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [Column("id_users")]
        public string idUsers { get; set; } = string.Empty;

        public virtual List<Product> Products { get; set; } = new List<Product>();
    }

}
