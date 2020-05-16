using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;
using Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecordController : ControllerBase
    {
        readonly IDataManager dataManager;

        public RecordController(IDataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecordModel>>> Get()
        {
            return await dataManager.Get();
        }

        [HttpGet("{Title}")]
        public async Task<ActionResult<RecordModel>> GetRecord(string Title)
        {
            return await dataManager.Get(Title);
        }

        [HttpGet("type/{Type}")]
        public async Task<ActionResult<IEnumerable<RecordModel>>> GetRecordsByType(string Type)
        {
            return await dataManager.GetByType(Type);
        }

        [HttpPost]
        public async Task<IActionResult> NewRecord([FromBody] RecordModel model)
        {
            if (await dataManager.Add(model))
                return Ok("new record successfully written");

            return StatusCode(400);
        }

        [HttpPatch("{Title}/comments")]
        public async Task<IActionResult> Put(string Title, [FromBody] string NewComment)
        {
            if (await dataManager.UpdateComment(Title, NewComment))
                return Ok("record successfully updated");

            return StatusCode(400);
        }

        [HttpDelete("{Title}")]
        public async Task<IActionResult> Delete(string Title)
        {
            if (await dataManager.Delete(Title))
                return Ok("record successfully deleted");

            return StatusCode(400);
        }
    }
}
