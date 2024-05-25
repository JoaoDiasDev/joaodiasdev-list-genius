using ListGenius.Api.Entities.Bases;
using ListGenius.Api.Entities.Products;
using ListGenius.Api.Entities.ProductShareds;
using ListGenius.Api.Entities.ProductSubGroups;

namespace ListGenius.Api.Entities.ProductGroups;

public sealed class ProductGroup : BaseEntity
{

    public string Description { get; set; } = string.Empty;
    public byte[] Image { get; set; } = [];
    public IEnumerable<ProductSubGroup> ProductSubGroups { get; set; } = [];
    public IEnumerable<Product> Products { get; set; } = [];
    public IEnumerable<ProductShared> ProductsShared { get; set; } = [];
    public ProductGroup() { }
}
