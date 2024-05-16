using JoaoDiasDev.ListGenius.Business.Interfaces;
using JoaoDiasDev.ListGenius.Data.VO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JoaoDiasDev.ListGenius.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private ILoginBusiness _loginBusiness;

        public AuthController(ILoginBusiness loginBusiness) { _loginBusiness = loginBusiness; }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [Route("signin")]
        public IActionResult SignIn([FromBody] UserVO user)
        {
            if (user is null)
            {
                return BadRequest("Invalid client request");
            }

            var token = _loginBusiness.ValidateCredentials(user);

            if (token is null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVo)
        {
            if (tokenVo is null)
            {
                return BadRequest("Invalid client request");
            }

            var token = _loginBusiness.ValidateCredentials(tokenVo);

            if (token is null)
            {
                return BadRequest("Invalid client request");
            }
            return Ok(token);
        }


        [HttpGet]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {
            var userName = User?.Identity?.Name;
            if (userName is null)
            {
                return BadRequest("Invalid client request");
            }
            var result = _loginBusiness.RevokeToken(userName);

            if (!result)
            {
                return BadRequest("Invalid client request");
            }
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("register")]
        [Authorize("Bearer")]
        public IActionResult Register([FromBody] UserRegisterVO user)
        {
            if (user is null)
            {
                return BadRequest("No User informed to register.");
            }

            var userExist = _loginBusiness.ValidateCredentials(user: user ?? new UserRegisterVO());
            if (userExist)
            {
                return BadRequest("User with same email or username already registered.");
            }

            if (!_loginBusiness.CreateUser(user ?? new UserRegisterVO()))
            {
                return BadRequest("Error when trying to creating user, contact support!");
            }

            return Ok("User registered with success");

        }
    }
}
