using ListGenius.Api.Entities.Bases;
using ListGenius.Api.Entities.ProductGroups;
using ListGenius.Api.Entities.Products.Enums;
using ListGenius.Api.Entities.ProductsLists;
using ListGenius.Api.Entities.ProductSubGroups;

namespace ListGenius.Api.Entities.Products;

public sealed class Product : BaseProduct
{
    public int IdProductsList { get; set; }

    public int IdProductGroup { get; set; }

    public int IdProductSubGroup { get; set; }

    public ProductsList ProductsList { get; set; } = new ProductsList();

    public ProductGroup ProductGroup { get; set; } = new ProductGroup();

    public ProductSubGroup ProductSubGroup { get; set; } = new ProductSubGroup();

    public Product() { }

    public Product(string name, bool enabled, DateTime createdDate, DateTime updatedDate,
                     decimal value, string description, byte[] qrcode, byte[] image, string link, UnitsOfMeasurement unit)
          : base(name, enabled, createdDate, updatedDate, value, description, qrcode, image, link, unit)
    {
    }

}
