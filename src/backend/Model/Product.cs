using JoaoDiasDev.ProductList.Data.Enums;
using JoaoDiasDev.ProductList.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoaoDiasDev.ProductList.Model
{
    [Table("product")]
    public class Product : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("value")]
        public decimal Value { get; set; }

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("qrcode")]
        public string qrcode { get; set; } = string.Empty;

        [Column("image")]
        public string Image { get; set; } = string.Empty;

        [Column("link")]
        public string Link { get; set; } = string.Empty;

        [Column("enabled")]
        public bool Enabled { get; set; } = true;

        [Column("unit")]
        public UnitOfMeasurement Unit { get; set; } = UnitOfMeasurement.Meter;

        public virtual ProductList ProductList { get; set; } = new ProductList();

    }
}
