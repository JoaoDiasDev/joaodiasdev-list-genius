using JoaoDiasDev.ProductList.Data.VO;

namespace JoaoDiasDev.ProductList.Business.Interfaces
{
    public interface ILoginBusiness
    {
        TokenVO ValidateCredentials(UserVO user);
        TokenVO ValidateCredentials(TokenVO token);
        bool RevokeToken(string userName);
    }
}
