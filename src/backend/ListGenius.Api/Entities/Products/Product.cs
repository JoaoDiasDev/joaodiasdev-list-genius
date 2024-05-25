using ListGenius.Api.Entities.Bases;
using ListGenius.Api.Entities.ProductGroups;
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


}
