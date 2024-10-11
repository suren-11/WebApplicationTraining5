using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplicationTraining5.Entities
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId ObjectId { get; set; } 

        [BsonElement("ids")] 
        public int Id { get; set; }

        [BsonElement("items")] 
        public List<OrderdItem> Items { get; set; }

        [BsonElement("employeeId")] 
        public int EmployeeId { get; set; }

        [BsonElement("totalPrice")] 
        public double TotalPrice { get; set; }

    }
}
