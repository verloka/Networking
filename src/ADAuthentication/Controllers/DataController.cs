using ADAuthentication.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ADAuthentication.Controllers
{
    [ADAuthorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DataController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Secret data!");
        }
    }
}
