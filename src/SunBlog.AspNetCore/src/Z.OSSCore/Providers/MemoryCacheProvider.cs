using Microsoft.Extensions.Caching.Memory;
using Z.OSSCore.Interface;

namespace Z.OSSCore.Providers
{
    /// <summary>
    /// 默认实现的缓存提供
    /// </summary>
    class MemoryCacheProvider : ICacheProvider
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheProvider(IMemoryCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(IMemoryCache));
        }

        public T Get<T>(string key) where T : class
        {
            return _cache.Get<T>(key);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void Set<T>(string key, T value, TimeSpan ts) where T : class
        {
            _cache.Set(key, value, ts);
        }
    }
}
