using ListGenius.Api.Entities.Bases;
using ListGenius.Api.Entities.ProductGroups;
using ListGenius.Api.Entities.Products;
using ListGenius.Api.Entities.ProductShareds;

namespace ListGenius.Api.Entities.ProductSubGroups;


public sealed class ProductSubGroup : BaseEntity
{
    public string Description { get; set; } = string.Empty;

    public byte[] Image { get; set; } = [];

    public int IdProductGroup { get; set; }

    public ProductGroup ProductGroup { get; set; } = new ProductGroup();

    public List<Product> Products { get; set; } = [];

    public List<ProductShared> ProductsShared { get; set; } = [];

    public ProductSubGroup() { }

    public ProductSubGroup(string name,
        string description,
        bool enabled,
        DateTime createdDate,
        DateTime updatedDate,
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
