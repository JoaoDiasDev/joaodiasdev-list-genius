using ListGenius.Web.Components.Users;
using Microsoft.AspNetCore.Components.Authorization;

namespace ListGenius.Web.Components.Auth;

public interface IAuthService
{
    Task<UserLoginResult?> Login(UserModel loginModel);
    Task Logout();
    Task<UserProfile?> GetUserProfile();
    Task<AuthenticationState> GetAuthenticationState();
    Task<bool> VerifyLoggedUser();
    Task<bool> UpdateLogoImage(UserUpdateImage userUpdateImage);
    Task<bool> UpdateProfileImage(UserUpdateImage userUpdateImage);
}
