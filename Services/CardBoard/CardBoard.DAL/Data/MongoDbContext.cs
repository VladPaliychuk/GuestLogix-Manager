using CardBoard.DAL.Entities;
using CardBoard.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CardBoard.DAL.Data
{
    public class CardBoardContext : ICardBoardContext
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Card> _collection;
        public CardBoardContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("mongodb://localhost:27017"));
            var _database = client.GetDatabase("CardBoardDb");

            _collection = _database.GetCollection<Card>("card");
            CardBoardSeed.SeedData(_collection);
        }
        public IMongoDatabase Database => _database;
        public IMongoCollection<Card> Cards => _collection;
    }
}
