using MongoDB.Bson.Serialization.Attributes;

namespace WebApplicationTraining5.Entities
{
    public class OrderdItem
    {
        [BsonElement("id")] 
        public int Id { get; set; }

        [BsonElement("qty")] 
        public int Qty { get; set; }

        [BsonElement("total")] 
        public double Total { get; set; }
    }
}
