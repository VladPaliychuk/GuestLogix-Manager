using BasketProducts.API.Data.Interfaces;
using BasketProducts.API.Entities;
using BasketProducts.API.Repository.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Driver;

namespace BasketProducts.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IBasketContext _context;
        public ProductRepository(IBasketContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                .Products
                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                   && deleteResult.DeletedCount > 0;
        }

        public async Task<Product> Get(string id)
        {
            return await _context
                .Products
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context
                .Products
                .Find(p => true)
                .ToListAsync();
        }

        public async Task Post(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> Update(Product product)
        {
            var updateResult = await _context
                .Products
                .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged
                   && updateResult.ModifiedCount > 0;
        }
    }
}
