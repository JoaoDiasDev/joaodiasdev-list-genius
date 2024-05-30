using ListGenius.Api.Entities.Bases;
using ListGenius.Api.Entities.Products;
using ListGenius.Api.Entities.ProductsShared;
using ListGenius.Api.Entities.ProductSubGroups;

namespace ListGenius.Api.Entities.ProductGroups;

public class ProductGroup : BaseEntity
{

    public string Description { get; init; } = string.Empty;
    public byte[] Image { get; init; } = [];
    public virtual ICollection<ProductSubGroup> ProductSubGroups { get; init; } = [];
    public virtual ICollection<Product> Products { get; init; } = [];
    public virtual ICollection<ProductShared> ProductsShared { get; init; } = [];
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
