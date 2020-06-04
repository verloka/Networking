using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Caching.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Caching.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProxyController : ControllerBase
    {
        readonly IDistributedCache cache;
        readonly IHttpClientFactory httpFactory;

        public ProxyController(IDistributedCache cache, IHttpClientFactory httpFactory)
        {
            this.cache = cache;
            this.httpFactory = httpFactory;
        }

        public IActionResult Get()
        {
            return Ok("Enter id to get a record");
        }

        [HttpGet]
        [Route("{Id:int}")]
        public async Task<IActionResult> Get(int Id)
        {
            Stopwatch st = new Stopwatch();
            st.Start();

            RecordModel data = null;
            string result = await cache.GetStringAsync($"{Id}-record");
            if (result != null)
            {
                data = JsonConvert.DeserializeObject<RecordModel>(result);
            }
            else
            {
                result = await GetResponseString($"remotestore/{Id}");
                await cache.SetStringAsync($"{Id}-record", result);
                data = JsonConvert.DeserializeObject<RecordModel>(result);
            }

            st.Stop();

            return new JsonResult(new { elapsed = TimeSpan.FromMilliseconds(st.ElapsedMilliseconds).ToString(), data });
        }

        [NonAction]
        async Task<string> GetResponseString(string path)
        {
            var client = httpFactory.CreateClient("Data");
            var request = new HttpRequestMessage(HttpMethod.Get, path);
            var response = await client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
