namespace ServerLibrary.Repositories.Contracts;

public interface IUserAccountRepository
{
    Task<GeneralResponse> CreateAsync(RegisterDto user);
    Task<LoginResponse> SignInAsync(LoginDto user);
    Task<LoginResponse> RefreshTokenAsync(RefreshTokenDto token);
}