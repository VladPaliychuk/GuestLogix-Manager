using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BasketProducts.API.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        [BsonElement("Name")]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal? Price { get; set; }
    }
}
