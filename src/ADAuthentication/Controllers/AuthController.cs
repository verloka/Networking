using ADAuthentication.Models;
using ADAuthentication.Services;
using Microsoft.AspNetCore.Mvc;

namespace ADAuthentication.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly UserService userService;

        public AuthController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login(AuthenticateRequest model)
        {
            var response = userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
    }
}
