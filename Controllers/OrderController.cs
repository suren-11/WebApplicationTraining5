using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using WebApplicationTraining5.DB;
using WebApplicationTraining5.Entities;

namespace WebApplicationTraining5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly MongoDb _mongoDb;

        public OrderController(MongoDb mongoDb)
        {
            _mongoDb = mongoDb;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _mongoDb.GetOrders();
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrder(Order order)
        {

            bool success = await _mongoDb.SaveOrder(order);
            if (success)
            {
                return Ok(order);
            }
            return BadRequest("Item not saved");
        }

    }
}
