

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplicationTraining5.Entities
{
    public class Item
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId ObjectId { get; set; }

        [BsonElement("id")]
        public int Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("price")]
        public double Price { get; set; }

        [BsonElement("created")]
        public DateTime Created { get; set; }

        [BsonElement("updated")]
        public DateTime Updated { get; set; }
    }
}
