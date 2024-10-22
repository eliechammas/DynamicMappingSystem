using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BLL.Caching
{
    public class CacheService : ICacheService
    {
        /// <summary>
        /// A distributed cache is a cache shared by multiple app servers
        /// A distributed cache has several advantages over other caching scenarios 
        /// where cached data is stored on individual app servers.
        /// </summary>
        private readonly IDistributedCache _cache;
        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        async Task<T?> ICacheService.GetAsync<T>(string cacheKey) where T : class
        {
            T? resultToReturn = default(T?);

            string? cacheValue = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cacheValue))
            {
                resultToReturn = JsonSerializer.Deserialize<T>(cacheValue);
            }

            return resultToReturn;
        }


        async Task ICacheService.SetAsync<T>(string cacheKey, T value)
        {
            string valueToChache = JsonSerializer.Serialize<T>(value);
            await _cache.SetStringAsync(cacheKey
                                        , valueToChache
                                        , new DistributedCacheEntryOptions
                                          {
                                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                                          });
        }


        async Task ICacheService.SetAsync<T>(string cacheKey, T value, int expiryTimeInMinutes)
        {
            string valueToChache = JsonSerializer.Serialize<T>(value);
            await _cache.SetStringAsync(cacheKey
                                        , valueToChache
                                        , new DistributedCacheEntryOptions
                                        {
                                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expiryTimeInMinutes)
                                        });
        }

        async Task ICacheService.RefreshAsync(string cacheKey)
        {
            await _cache.RefreshAsync(cacheKey);
        }

        async Task ICacheService.RemoveAsync(string cacheKey)
        {
            await _cache.RemoveAsync(cacheKey);
        }

    }
}
