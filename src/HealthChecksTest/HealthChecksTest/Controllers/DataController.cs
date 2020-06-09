using System.Threading;
using Microsoft.AspNetCore.Mvc;

namespace HealthChecksTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        int BeginLatency = 1;
        static int Latency = 1;

        [HttpGet]
        public IActionResult Get()
        {
            Thread.Sleep(Latency += 1000);
            return Ok("Data");
        }

        [HttpGet]
        [Route("reset")]
        public IActionResult Reset()
        {
            Latency = BeginLatency;
            return Ok();
        }
    }
}
