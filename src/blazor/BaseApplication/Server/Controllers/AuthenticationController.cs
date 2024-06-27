namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(IUserAccountRepository accountInterface) : ControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
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
    [AllowAnonymous]
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
    [AllowAnonymous]
    public async Task<IActionResult> RefreshTokenAsync(RefreshTokenDto token)
    {
        if (token is null)
        {
            return BadRequest("Model is empty");
        }
        var result = await accountInterface.RefreshTokenAsync(token);
        return Ok(result);
    }

    [HttpGet("users")]
    [Authorize]
    public async Task<IActionResult> GetUsersAsync()
    {
        var users = await accountInterface.GetUsers();
        if (users is null) return NotFound();
        return Ok(users);
    }

    [HttpPut("update-user")]
    [Authorize]
    public async Task<IActionResult> UpdateUser(ManageUserDto user)
    {
        var result = await accountInterface.UpdateUser(user);
        return Ok(result);
    }

    [HttpGet("roles")]
    [Authorize]
    public async Task<IActionResult> GetRoles()
    {
        var users = await accountInterface.GetRoles();
        if (users is null) return NotFound();
        return Ok(users);
    }

    [HttpDelete("delete-user/{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var result = await accountInterface.DeleteUser(id);
        return Ok(result);
    }

    [HttpGet("user-image/{id}")]
    [Authorize]
    public async Task<IActionResult> GetUserImage(int id)
    {
        var result = await accountInterface.GetUserImage(id);
        return Ok(result);
    }

    [HttpPut("update-profile")]
    [Authorize]
    public async Task<IActionResult> UpdateProfile(UserProfileDto profile)
    {
        var result = await accountInterface.UpdateProfile(profile);
        return Ok(result);
    }
}