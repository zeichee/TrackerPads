using System;
using System.Threading.Tasks;
using Oosa.Caching;

namespace Vsslabs.Bll.Services
{
    public static class CachingServiceExtensions
    {
        public static Task<T> GetAsync<T>(this ICachingService cachingService, string key, Func<Task<T>> acquire) where T : class
        {
            return GetAsync(cachingService, key, 60, acquire);
        }

        public static async Task<T> GetAsync<T>(this ICachingService cachingService, string key, int cacheTime, Func<Task<T>> acquire) where T : class
        {
            if (cachingService.Exists(key))
            {
                return cachingService.Get<T>(key);
            }

            var result = await acquire();
            cachingService.Add(result, key, cacheTime);
            return result;
        }
    }
}
