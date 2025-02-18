using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using ShopServer.Business.Inteface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopServer.Business.Implement
{
    public class CachingBusiness : ICachingBusiness
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;

        public CachingBusiness(IMemoryCache memoryCache, IConfiguration configuration)
        {
            _memoryCache = memoryCache;
            _configuration = configuration;
        }

        public string Get(string key)
        {
            var found = _memoryCache.TryGetValue(key, out var value);
            return found ? value.ToString() : null;
        }
        public List<T> Get<T>(string key)
        {
            var found = _memoryCache.TryGetValue(key, out var value);
            return found ? (List<T>)value : null;
        }
        public void Upsert<T>(string key, T value)
        {
            try
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                 .SetAbsoluteExpiration(TimeSpan.FromSeconds(Int32.Parse(_configuration["cache-expire"])));
                var s = _memoryCache.Set(key, value, cacheEntryOptions);
            }
            catch (Exception ex)
            {
                // deal with the exception, or log error
            }
        }

        public void Delete(string key)
        {
            {
                try
                {
                    _memoryCache.Remove(key);
                }
                catch (Exception ex)
                {
                    // Deal with the exception
                    throw;
                }
            }
        }
    }
}
