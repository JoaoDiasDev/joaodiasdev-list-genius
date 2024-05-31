using Microsoft.AspNetCore.Identity;

namespace ListGenius.Api.Entities.Users;

public class ApplicationUser : IdentityUser
{
    [StringLength(100)]
    public string FullName { get; init; } = string.Empty;
    public byte[] LogoImage { get; init; } = [];
    public byte[] ProfilePicture { get; init; } = [];

    public virtual ICollection<ProductsList> ProductsLists { get; init; } = [];
}
