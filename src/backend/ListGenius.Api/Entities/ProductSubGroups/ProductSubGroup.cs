using ListGenius.Api.Entities.Bases;
using ListGenius.Api.Entities.ProductGroups;
using ListGenius.Api.Entities.Products;
using ListGenius.Api.Entities.ProductsShared;

namespace ListGenius.Api.Entities.ProductSubGroups;


public class ProductSubGroup : BaseEntity
{
    public string Description { get; init; } = string.Empty;

    public byte[] Image { get; init; } = [];

    public int IdProductGroup { get; set; }

    public virtual ProductGroup ProductGroup { get; set; } = new();

    public virtual ICollection<Product> Products { get; init; } = [];

    public virtual ICollection<ProductShared> ProductsShared { get; init; } = [];

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
