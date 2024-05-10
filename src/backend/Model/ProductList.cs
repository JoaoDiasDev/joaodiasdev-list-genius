using JoaoDiasDev.ProductList.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoaoDiasDev.ProductList.Model
{
    [Table("ProductsList")]
    public class ProductList : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("enabled")]
        public bool Enabled { get; set; } = true;

        [Column("total_products_count")]
        public int TotalProductsCount { get; set; } = 0;

        [Column("total_products_value")]
        public decimal TotalProductsValue { get; set; } = 0.0M;

        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public virtual List<Product> Products { get; set; } = new List<Product>();
    }

}
