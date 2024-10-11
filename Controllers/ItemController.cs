using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTraining5.DB;
using WebApplicationTraining5.Entities;

namespace WebApplicationTraining5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly MongoDb _mongoDb;

        public ItemController(MongoDb mongoDb)
        {
            _mongoDb = mongoDb;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var items = await _mongoDb.GetItems();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> SaveItem(Item item)
        {
            bool success = await _mongoDb.SaveItem(item);
            if (success)
            {
                return Ok(item);
            }
            return BadRequest("Item not saved");
        }
    }
}
