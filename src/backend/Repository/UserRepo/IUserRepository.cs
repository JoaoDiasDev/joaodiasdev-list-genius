using JoaoDiasDev.ProductList.Data.VO;
using JoaoDiasDev.ProductList.Model;

namespace JoaoDiasDev.ProductList.Repository.UserRepo
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);
        User ValidateCredentials(string userName);
        bool RevokeToken(string userName);
        User RefreshUserInfo(User user);
    }
}
