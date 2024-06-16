namespace ClientLibrary.Services.Contracts;

public interface IUserAccountService
{
    Task<GeneralResponse> CreateAsync(RegisterDto user);
    Task<LoginResponse> SignInAsync(LoginDto user);
    Task<LoginResponse> RefreshTokenAsync(RefreshTokenDto refreshToken);
}