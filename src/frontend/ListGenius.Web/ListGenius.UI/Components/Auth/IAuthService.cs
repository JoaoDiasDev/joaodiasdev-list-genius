using ListGenius.UI.Components.Users;

namespace ListGenius.UI.Components.Auth;

public interface IAuthService
{
    Task<UserLoginResult?> Login(UserModel loginModel);

    Task Logout();
}
