using BasketProducts.API.Data.Interfaces;
using BasketProducts.API.Entities;
using MongoDB.Driver;

namespace BasketProducts.API.Data
{
    public class BasketContext : IBasketContext
    {
        public BasketContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        }

        public IMongoCollection<Product> Products { get; }
    }
}
