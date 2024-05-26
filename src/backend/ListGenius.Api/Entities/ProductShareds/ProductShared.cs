using ListGenius.Api.Entities.Bases;
using ListGenius.Api.Entities.ProductGroups;
using ListGenius.Api.Entities.Products.Enums;
using ListGenius.Api.Entities.ProductSubGroups;

namespace ListGenius.Api.Entities.ProductShareds;

public sealed class ProductShared : BaseProduct
{
    public int IdProductGroup { get; set; }

    public int IdProductSubGroup { get; set; }

    public ProductGroup ProductGroup { get; set; } = new ProductGroup();

    public ProductSubGroup ProductSubGroup { get; set; } = new ProductSubGroup();

    public ProductShared() { }

    public ProductShared(string name, bool enabled, DateTime createdDate, DateTime updatedDate,
                    decimal value, string description, byte[] qrcode, byte[] image, string link, UnitsOfMeasurement unit)
         : base(name, enabled, createdDate, updatedDate, value, description, qrcode, image, link, unit)
    {
    }
}
