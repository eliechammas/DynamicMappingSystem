using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Caching
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string cacheKey, CancellationToken token = default) where T:class;
        Task SetAsync<T>(string cacheKey, T value, CancellationToken token = default) where T:class;
        Task RemoveAsync<T>(string cacheKey, CancellationToken token = default) where T:class;
    }
}
