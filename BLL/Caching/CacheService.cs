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
        private readonly IDistributedCache _cache;
        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        async Task<T?> ICacheService.GetAsync<T>(string cacheKey, System.Threading.CancellationToken token = default) where T : class
        {
            T? resultToReturn = default(T?);

            string? cacheValue = await _cache.GetStringAsync(cacheKey, token);
            if (!string.IsNullOrEmpty(cacheValue))
            {
                resultToReturn = JsonSerializer.Deserialize<T>(cacheValue);
            }

            return resultToReturn;
        }


        async Task ICacheService.SetAsync<T>(string cacheKey, T value, System.Threading.CancellationToken token = default)
        {
            string valueToChache = JsonSerializer.Serialize<T>(value);
            await _cache.SetStringAsync(cacheKey, valueToChache, token);
        }

        async Task ICacheService.RemoveAsync<T>(string cacheKey, CancellationToken token = default)
        {
            await _cache.RemoveAsync(cacheKey, token);
        }
    }
}
