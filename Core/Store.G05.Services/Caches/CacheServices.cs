using Store.G05.Domain.Contracts;
using Store.G05.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G05.Services.Caches
{
    public class CacheServices(ICacheRepositry cacheRepositry) : ICacheServices
    {
        public async Task<string?> GetCacheValueAsync(string key)
        {
            var value = await cacheRepositry.GetAsync(key);
            return value == null ? null : value;
        }

        public async Task SetCacheValueAsync(string key, object value, TimeSpan duration)
        {
            await cacheRepositry.SetAsync(key, value, duration);    
        }
    }
}
