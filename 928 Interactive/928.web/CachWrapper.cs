using _928.Core.Interfaces;
using System.Runtime.Caching;
using System.Web;

namespace _928.Web {
    public class CachWrapper : ICache{

        public object this[string name] {
            get {
                return MemoryCache.Default.Get(name);
            }
            set {
                MemoryCache.Default.Set(new CacheItem(name, value), new CacheItemPolicy() { AbsoluteExpiration = ObjectCache.InfiniteAbsoluteExpiration });
            }
        }

        public int Count {
            get { return HttpRuntime.Cache.Count; }
        }
    }
}
