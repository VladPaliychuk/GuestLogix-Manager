using Basket.API.Entities;
using Basket.API.Repository.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repository
{
    public class ClientBasketRepository : IClientBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public ClientBasketRepository(IDistributedCache cache)
        {
            _redisCache = cache ?? throw new ArgumentNullException(nameof(cache));
        }
        public async Task DeleteBasket(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }

        public async Task<ProductList> GetBasketAsync(string userName)
        {
            var product = await _redisCache.GetStringAsync(userName);

            if (String.IsNullOrEmpty(product))
                return null;
            return JsonConvert.DeserializeObject<ProductList>(product);
        }

        public async Task<ProductList> UpdateBasketAsync(ProductList productList)
        {
            await _redisCache.SetStringAsync(productList.UserName, JsonConvert.SerializeObject(productList));

            return await GetBasketAsync(productList.UserName);
        }
    }
}
