using ListGenius.Web.Components.Users;

namespace ListGenius.Web.Components.Auth;

public interface IAuthService
{
    Task<UserLoginResult?> Login(UserModel loginModel);

    Task Logout();
}
