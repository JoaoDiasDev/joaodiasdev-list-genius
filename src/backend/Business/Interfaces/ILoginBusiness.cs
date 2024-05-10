using JoaoDiasDev.ListGenius.Data.VO;

namespace JoaoDiasDev.ListGenius.Business.Interfaces
{
    public interface ILoginBusiness
    {
        TokenVO ValidateCredentials(UserVO user);
        TokenVO ValidateCredentials(TokenVO token);
        bool RevokeToken(string userName);
    }
}
