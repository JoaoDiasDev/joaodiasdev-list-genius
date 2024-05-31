using ListGenius.Api.Entities.Products.Enums;

namespace ListGenius.Api.Entities.ProductsShared;

public class ProductShared : BaseProduct
{
    public int IdProductGroup { get; set; }

    public int IdProductSubGroup { get; set; }

    public virtual ProductGroup ProductGroup { get; set; } = new();

    public virtual ProductSubGroup ProductSubGroup { get; set; } = new();

    public ProductShared() { }

    public ProductShared(string name, bool enabled, DateTime createdDate, DateTime updatedDate,
                    decimal value, string description, byte[] qrcode, byte[] image, string link, UnitsOfMeasurement unit)
         : base(name, enabled, createdDate, updatedDate, value, description, qrcode, image, link, unit)
    {
    }
}
