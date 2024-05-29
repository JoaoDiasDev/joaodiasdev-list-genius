using ListGenius.Api.Entities.Bases;
using ListGenius.Api.Entities.Products;
using ListGenius.Api.Entities.Users;
using ListGenius.Api.Validation;

namespace ListGenius.Api.Entities.ProductsLists;

public sealed class ProductsList : BaseEntity
{
    public string Description { get; private set; } = string.Empty;

    public byte[] Image { get; private set; } = [];

    public bool Public { get; private set; } = false;

    public string ExternalLink { get; private set; } = string.Empty;

    public string IdUser { get; set; } = string.Empty;

    public List<Product> Products { get; set; } = [];

    public ApplicationUser User { get; set; } = new ApplicationUser();

    public ProductsList() { }

    public ProductsList(string name, List<Product> products)
    {
        ValidateDomain(
            name,
            products);
    }
    public ProductsList(int id, string name, List<Product> products)
    {
        DomainExceptionValidation.When(id < 0, "Id inválido.");
        Id = id;
        ValidateDomain(
            name,
            products);
    }
    private void ValidateDomain(string name, List<Product> products)
    {
        DomainExceptionValidation.When(
            string.IsNullOrEmpty(name),
           "Nome é obrigatório");
        DomainExceptionValidation.When(
            name.Length < 3,
           "Nome inválido");
        Name = name;

        DomainExceptionValidation.When(
            products.Count() < 1,
           "Shopping lists precisam ter pelo menos um produto");
        Products = products;
    }
}
