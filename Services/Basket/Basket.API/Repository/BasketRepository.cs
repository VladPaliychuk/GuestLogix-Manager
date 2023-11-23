using Basket.API.Entities;
using Basket.API.Repository.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache cache)
        {
            _redisCache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<CardList> GetCardListAsync(string userName)
        {
            var card = await _redisCache.GetStringAsync(userName);

            if (String.IsNullOrEmpty(card))
                return null;
            return JsonConvert.DeserializeObject<CardList>(card);
        }
        public async Task<CardList> UpdateCardListAsync(CardList cardList)
        {
            await _redisCache.SetStringAsync(cardList.UserName, JsonConvert.SerializeObject(cardList));

            return await GetCardListAsync(cardList.UserName);
        }
        public async Task DeleteCardListAsync(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }
    }
}
