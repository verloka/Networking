using System.Threading;
using Caching.Models;
using Microsoft.AspNetCore.Mvc;

namespace Caching.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RemoteStoreController : ControllerBase
    {
        [HttpGet]
        [Route("{Id:int}")]
        public IActionResult Get(int Id)
        {
            Thread.Sleep(5000);
            return Ok(new RecordModel(Id));
        }
    }
}
