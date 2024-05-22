using Microsoft.AspNetCore.Identity;

namespace ListGenius.Domain.Context
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public byte[] ProfileImage { get; set; } = [];

        public byte[] LogoImage { get; set; } = [];
    }

}
