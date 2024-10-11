using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using WebApplicationTraining5.Entities;

namespace WebApplicationTraining5.DB
{
    public class MongoDb
    {
        private readonly IMongoCollection<Item> _items;
        private readonly IMongoCollection<Order> _orders;
        private readonly IMongoDatabase _database;

        public MongoDb(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("Dbconnection"));
            _database = client.GetDatabase("WebApiTraining5");
            _items = _database.GetCollection<Item>("Items");
            _orders = _database.GetCollection<Order>("Orders");
        }

        public async Task<List<Item>> GetItems()
        {
            return await _items.Find(_ => true).ToListAsync();
        }

        public async Task<bool> SaveItem(Item item)
        {
            List<Item> items = GetItems().Result;
            if (items.Count > 0 && items != null)
            {
                Item item1 = items.Last();
                item.Id = item1.Id + 1;
            }
            else 
            {
                item.Id = 1;
            }
            await _items.InsertOneAsync(item);
            return true;
        }

        public async Task<UpdateResult> UpdateItem(Item item)
        {
            var filter = Builders<Item>.Filter.Eq(i=>i.Id , item.Id);
            var update = Builders<Item>.Update
                .Set(i=> i.Name, item.Name)
                .Set(i=>i.Description, item.Description)
                .Set(i=>i.Price, item.Price)
                .Set(i=>i.Updated, item.Updated);
            return await _items.UpdateOneAsync(filter,update);
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _orders.Find(_ => true).ToListAsync();
        }

        public async Task<bool> SaveOrder(Order order)
        {
            List<Order> orders = GetOrders().Result;
            if (orders.Count > 0 && orders != null)
            {
                Order order1 = orders.Last();
                order.Id = order1.Id + 1;
            }
            else
            {
                order.Id = 1;
            }
            await _orders.InsertOneAsync(order);
            return true;
        }
    }
}
