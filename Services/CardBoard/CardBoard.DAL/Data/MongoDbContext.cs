using CardBoard.DAL.Entities;
using CardBoard.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CardBoard.DAL.Data
{
    public class CardBoardContext : ICardBoardContext
    {
        public CardBoardContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("mongodb://localhost:27017"));
            var database = client.GetDatabase("CardBoardDb");

            Cards = database.GetCollection<Card>("Cards");
            CardBoardSeed.SeedData(Cards);
        }
        public IMongoCollection<Card> Cards { get; }
    }
}
