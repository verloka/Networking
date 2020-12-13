using JWTAuthentication2.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthentication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [JWTAuthorize]
    public class DataController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Secret data!");
        }
    }
}
