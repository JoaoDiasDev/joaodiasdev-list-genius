using JoaoDiasDev.ListGenius.Data.Enums;
using JoaoDiasDev.ListGenius.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoaoDiasDev.ListGenius.Model
{
    [Table("products")]
    public class Product : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("value")]
        public decimal Value { get; set; } = decimal.Zero;

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

        public virtual ProductsList ProductList { get; set; } = new ProductsList();

    }
}
