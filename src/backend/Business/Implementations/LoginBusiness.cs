using JoaoDiasDev.ProductList.Business.Interfaces;
using JoaoDiasDev.ProductList.Configurations;
using JoaoDiasDev.ProductList.Data.VO;
using JoaoDiasDev.ProductList.Repository.UserRepo;
using JoaoDiasDev.ProductList.Services.Token.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JoaoDiasDev.ProductList.Business.Implementations
{
    public class LoginBusiness : ILoginBusiness
    {
        private const string DATE_FORMAT = "yyyy-MM-dd";

        private TokenConfiguration _configuration;
        private IUserRepository _repository;
        private readonly ITokenService _tokenService;

        public LoginBusiness(
            TokenConfiguration configuration,
            IUserRepository repository,
            ITokenService tokenService)
        {
            _configuration = configuration;
            _repository = repository;
            _tokenService = tokenService;
        }

        public TokenVO ValidateCredentials(UserVO userCredentials)
        {
            var user = _repository.ValidateCredentials(userCredentials);
            if (user == null)
            {
                return new TokenVO(accessToken: "", refreshToken: "");
            }
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, userCredentials.UserName)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenUntilExpirationTime = DateTime.Now.AddDays(_configuration.DaysUntilExpiration);

            _repository.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.MinutesUntilExpiration);

            return new TokenVO(
                accessToken,
                refreshToken,
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT));
        }

        public TokenVO ValidateCredentials(TokenVO token)
        {
            var accessToken = token.AccessToken ?? string.Empty;
            var refreshToken = token.RefreshToken ?? string.Empty;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

            var userName = principal?.Identity?.Name ?? string.Empty;

            var user = _repository.ValidateCredentials(userName);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenUntilExpirationTime <= DateTime.Now)
            {
                return new TokenVO(accessToken: "", refreshToken: "");
            }

            accessToken = _tokenService.GenerateAccessToken(principal?.Claims ?? new ClaimsPrincipal().Claims);
            refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            _repository.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.MinutesUntilExpiration);

            return new TokenVO(
                accessToken,
                refreshToken,
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT));
        }

        public bool RevokeToken(string userName)
        {
            return _repository.RevokeToken(userName);
        }
    }
}
