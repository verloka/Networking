using System.Linq;
using JWTAuthentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthentication.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DataController : Controller
    {
        [Route("secret")]
        public IActionResult Get()
        {
            var validClaims = SecurityService.GetClaims().Select(x => x.Type);
            var userClaims = HttpContext.User.Claims.Select(x => x.Type);

            if (validClaims.Intersect(userClaims).Count() < 1)
                return StatusCode(403);

            return Ok("This message is top secret!");
        }
    }
}
