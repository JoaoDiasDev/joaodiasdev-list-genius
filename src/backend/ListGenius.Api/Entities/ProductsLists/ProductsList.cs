using ListGenius.Api.Entities.Users;

namespace ListGenius.Api.Entities.ProductsLists;

public class ProductsList : BaseEntity
{
    public string Description { get; private set; } = string.Empty;

    public byte[] Image { get; private set; } = [];

    public bool Public { get; private set; } = false;

    public string ExternalLink { get; private set; } = string.Empty;

    public string IdUser { get; set; } = string.Empty;

    public virtual ICollection<Product> Products { get; set; } = [];

    public virtual ApplicationUser? User { get; set; } = new();
}
