using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CardBoard.DAL.Entities
{
    public class Card
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public int Number { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Mail { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Age { get; set; } = null!;
        public string City { get; set; } = null!;
        public int Bonuses { get; set; }
    }
}
