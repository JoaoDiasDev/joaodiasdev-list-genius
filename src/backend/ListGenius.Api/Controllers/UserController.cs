using ListGenius.Api.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ListGenius.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;

    public UserController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> CreateUser([FromBody] User model)
    {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FullName = model.FullName };

        var result = await _userManager.CreateAsync(user, model.Password ?? string.Empty);

        if (result.Succeeded)
        {
            return Ok(model);
        }
        else
        {
            return BadRequest("Usuário ou senha inválidos");
        }
    }

    [HttpPost("Login")]
    public async Task<ActionResult<UserToken>> Login([FromBody] User userInfo)
    {
        var result = await _signInManager.PasswordSignInAsync(userInfo.Email,
                         userInfo.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return BuildToken(userInfo);
        }
        else
        {
            ModelState.AddModelError(string.Empty, "login inválido.");
            return BadRequest(ModelState);
        }
    }

    private UserToken BuildToken(User userInfo)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
            new Claim("joaodiasdev", "http://joaodiasdev.com"),
            new Claim(JwtRegisteredClaimNames.Aud, _configuration["Jwt:Audience"] ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Iss, _configuration["Jwt:Issuer"] ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"] ?? string.Empty));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.UtcNow.AddDays(7);

        JwtSecurityToken token = new(
           issuer: null,
           audience: null,
           claims: claims,
           expires: expiration,
           signingCredentials: creds);

        return new UserToken()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
    }
}