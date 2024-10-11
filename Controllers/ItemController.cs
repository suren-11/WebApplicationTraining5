using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
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
            item.Created = DateTime.UtcNow;
            item.Updated = DateTime.UtcNow;

            bool success = await _mongoDb.SaveItem(item);
            if (success)
            {
                return Ok(item);
            }
            return BadRequest("Item not saved");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateItem(Item item)
        {
            item.Updated = DateTime.UtcNow;

            UpdateResult success = await _mongoDb.UpdateItem(item);
            if (success != null)
            {
                return Ok(item);
            }
            else
            {
                return BadRequest("Not updated");
            }
        }
    }
}
