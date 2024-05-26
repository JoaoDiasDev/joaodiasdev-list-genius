using ListGenius.Api.Entities.Bases;
using ListGenius.Api.Entities.Products;
using ListGenius.Api.Entities.ProductShareds;
using ListGenius.Api.Entities.ProductSubGroups;

namespace ListGenius.Api.Entities.ProductGroups;

public sealed class ProductGroup : BaseEntity
{

    public string Description { get; set; } = string.Empty;
    public byte[] Image { get; set; } = [];
    public List<ProductSubGroup> ProductSubGroups { get; set; } = [];
    public List<Product> Products { get; set; } = [];
    public List<ProductShared> ProductsShared { get; set; } = [];
    public ProductGroup() { }
    public ProductGroup(string name,
        string description, DateTime
        createdDate, DateTime
        updatedDate,
        bool enabled,
        byte[] image)
    {
        Name = name;
        Description = description;
        Enabled = enabled;
        CreatedDate = createdDate;
        UpdatedDate = updatedDate;
        Image = image;
    }
}
