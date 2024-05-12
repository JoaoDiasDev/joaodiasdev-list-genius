using JoaoDiasDev.ListGenius.Data.VO;

namespace JoaoDiasDev.ListGenius.Business.Interfaces
{
    public interface ILoginBusiness
    {
        TokenVO ValidateCredentials(UserVO user);
        bool ValidateCredentials(UserRegisterVO user);
        TokenVO ValidateCredentials(TokenVO token);
        bool RevokeToken(string userName);
        bool CreateUser(UserRegisterVO user);
    }
}
