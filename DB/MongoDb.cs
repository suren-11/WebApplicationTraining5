using MongoDB.Driver;
using WebApplicationTraining5.Entities;

namespace WebApplicationTraining5.DB
{
    public class MongoDb
    {
        private readonly IMongoCollection<Item> _items;
        private readonly IMongoDatabase _database;

        public MongoDb(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("Dbconnection"));
            _database = client.GetDatabase("WebApiTraining5");
            _items = _database.GetCollection<Item>("Items");
        }

        public async Task<List<Item>> GetItems()
        {
            return await _items.Find(_ => true).ToListAsync();
        }

        public async Task<bool> SaveItem(Item item)
        {
            await _items.InsertOneAsync(item);
            return true;
        }
    }
}
