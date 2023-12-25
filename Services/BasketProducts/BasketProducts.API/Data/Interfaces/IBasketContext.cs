using BasketProducts.API.Entities;
using MongoDB.Driver;

namespace BasketProducts.API.Data.Interfaces
{
    public interface IBasketContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
