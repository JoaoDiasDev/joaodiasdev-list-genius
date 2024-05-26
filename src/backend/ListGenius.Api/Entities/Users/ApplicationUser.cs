using ListGenius.Api.Entities.ProductsLists;
using Microsoft.AspNetCore.Identity;

namespace ListGenius.Api.Entities.Users;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public byte[] LogoImage { get; set; } = [];
    public byte[] ProfilePicture { get; set; } = [];

    public List<ProductsList> ProductsLists { get; set; } = [];

    public ApplicationUser() { }
}
