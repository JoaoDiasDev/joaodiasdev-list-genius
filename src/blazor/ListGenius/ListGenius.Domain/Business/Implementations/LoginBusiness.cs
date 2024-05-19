using ListGenius.Domain.Business.Interfaces;

namespace ListGenius.Domain.Business.Implementations
{
    public class LoginBusiness : ILoginBusiness
    {
        //private const string DATE_FORMAT = "dd-MM-yyyy";
        //private TokenConfiguration _configuration;
        //private IUserRepository _repository;
        //private readonly ITokenService _tokenService;
        //private readonly UserRegisterConverter _userRegisterConverter;

        //public LoginBusiness(
        //    TokenConfiguration configuration,
        //    IUserRepository repository,
        //    ITokenService tokenService)
        //{
        //    _configuration = configuration;
        //    _repository = repository;
        //    _tokenService = tokenService;
        //    _userRegisterConverter = new UserRegisterConverter();
        //}

        //public TokenVO ValidateCredentials(UserVO userCredentials)
        //{
        //    var user = _repository.ValidateCredentials(userCredentials);
        //    if (user == null)
        //    {
        //        return new TokenVO(accessToken: "", refreshToken: "");
        //    }
        //    var claims = new List<Claim>
        //    {
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
        //        new Claim(JwtRegisteredClaimNames.UniqueName, userCredentials.UserName)
        //    };

        //    var accessToken = _tokenService.GenerateAccessToken(claims);
        //    var refreshToken = _tokenService.GenerateRefreshToken();

        //    user.RefreshToken = refreshToken;
        //    user.RefreshTokenUntilExpirationTime = DateTime.Now.AddDays(_configuration.DaysUntilExpiration);

        //    _repository.RefreshUserInfo(user);

        //    DateTime createDate = DateTime.Now;
        //    DateTime expirationDate = createDate.AddDays(_configuration.DaysUntilExpiration);

        //    return new TokenVO(
        //        accessToken,
        //        refreshToken,
        //        true,
        //        createDate.ToString(DATE_FORMAT),
        //        expirationDate.ToString(DATE_FORMAT));
        //}

        //public TokenVO ValidateCredentials(TokenVO token)
        //{
        //    var accessToken = token.AccessToken ?? string.Empty;
        //    var refreshToken = token.RefreshToken ?? string.Empty;

        //    var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

        //    var userName = principal?.Identity?.Name ?? string.Empty;

        //    var user = _repository.ValidateCredentials(userName: userName, email: string.Empty);

        //    if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenUntilExpirationTime <= DateTime.Now)
        //    {
        //        return new TokenVO(accessToken: "", refreshToken: "");
        //    }

        //    accessToken = _tokenService.GenerateAccessToken(principal?.Claims ?? new ClaimsPrincipal().Claims);
        //    refreshToken = _tokenService.GenerateRefreshToken();

        //    user.RefreshToken = refreshToken;

        //    _repository.RefreshUserInfo(user);

        //    DateTime createDate = DateTime.Now;
        //    DateTime expirationDate = createDate.AddDays(_configuration.DaysUntilExpiration);

        //    return new TokenVO(
        //        accessToken,
        //        refreshToken,
        //        true,
        //        createDate.ToString(DATE_FORMAT),
        //        expirationDate.ToString(DATE_FORMAT));
        //}

        //public bool RevokeToken(string userName)
        //{
        //    return _repository.RevokeToken(userName);
        //}

        //public bool ValidateCredentials(UserRegisterVO user)
        //{
        //    return _repository.ValidateCredentials(userName: user.UserName, email: user.Email).Id is not 0;
        //}

        //public bool CreateUser(UserRegisterVO userRegister)
        //{
        //    var entity = _userRegisterConverter.Parse(userRegister);
        //    if (entity != null)
        //    {
        //        return _repository.CreateUser(entity);
        //    }
        //    return true;
        //}
    }
}
