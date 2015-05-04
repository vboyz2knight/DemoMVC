using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;

namespace DemoMVC.Classes
{
    public class DefaultCacheFileDependencyProvider : ICacheProvider
    {
        private ObjectCache Cache { get { return MemoryCache.Default;} }

        public object Get(string key)
        {
            return Cache[key];
        }

        public void Set(string key, object data, int cacheTime)
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);

            Cache.Add(new CacheItem(key, data), policy);
        }

        public void Set(string key, object data, string filePath)
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.ChangeMonitors.Add(new HostFileChangeMonitor(new List<string> { filePath }));

            Cache.Add(new CacheItem(key, data), policy);
        }

        public bool IsSet(string key)
        {
            return Cache[key] != null;
        }

        public void Invalidate(string key)
        {
            Cache.Remove(key);
        }
    }
}