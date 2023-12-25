using BasketProducts.API.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace BasketProducts.API.Services
{
    public class CachingService : ICachingService
    {
        private MemoryCache _memoryCache = new MemoryCache(new MemoryCacheOptions()
        {
            SizeLimit = 1024 * 1024 * 1024
        });

        public T? GetData<T>(string key)
        {
            try
            {
                //T item = (T)_memoryCache.Get(key);

                //return item;
                if (_memoryCache.TryGetValue(key, out T? item))
                {
                    return item;
                }
                return default;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve data from cache.", ex);
            }
        }

        public object RemoveData(string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new Exception("Key is empty or null;");
                }

                _memoryCache.Remove(key);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to remove data from cache.", ex);
            }
        }

        public T SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSize(20) // Встановлює розмір (вагу) елемента кешу в байтах.
                .SetPriority(CacheItemPriority.High) // Встановлює пріоритет елемента кешу. 
                .SetSlidingExpiration(expirationTime - DateTimeOffset.Now) // Встановлює змінний термін дії для елемента кешу.
                                                                 // Якщо елемент не використовується протягом цього часу, він буде видалений.
                                                                 // Якщо елемент активний, термін дії буде автоматично продовжуватися.
                .SetAbsoluteExpiration(expirationTime); // Встановлює абсолютний термін дії для елемента кешу.

            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    _memoryCache.Set(key, value, cacheEntryOptions);
                }
                return value;
                //T item = (T)_memoryCache.Get(key);
                //return item;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to set data in cache.", ex);
            }
        }
    }
}
