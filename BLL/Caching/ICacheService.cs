using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Caching
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string cacheKey) where T:class;
        Task SetAsync<T>(string cacheKey, T value) where T:class;
        Task SetAsync<T>(string cacheKey, T value, int expiryTimeInMinutes) where T : class;
        Task RefreshAsync(string cacheKey);
        Task RemoveAsync(string cacheKey);
    }
}
