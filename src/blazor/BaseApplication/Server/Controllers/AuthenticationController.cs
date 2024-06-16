namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthenticationController(IUserAccountRepository accountInterface) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> CreateAsync(RegisterDto user)
    {
        if (user is null)
        {
            return BadRequest("Model is empty");
        }
        var result = await accountInterface.CreateAsync(user);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> SignInAsync(LoginDto user)
    {
        if (user is null)
        {
            return BadRequest("Model is empty");
        }
        var result = await accountInterface.SignInAsync(user);
        return Ok(result);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshTokenAsync(RefreshTokenDto token)
    {
        if (token is null)
        {
            return BadRequest("Model is empty");
        }
        var result = await accountInterface.RefreshTokenAsync(token);
        return Ok(result);
    }
}