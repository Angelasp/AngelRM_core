
namespace Angel.DataAccess
{
    public interface ICacheService
    {
        bool Exists(string key);
        bool Set(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte);
        bool Set(string key, object value, TimeSpan expiresIn, bool isSliding = false);
        void Set(string key, object value);
        void Set(string key, object value, TimeSpan ts);
        void Set(string key, object value, int seconds);
        void Remove(string key);
        void RemoveAll(IEnumerable<string> keys);
        T Get<T>(string key);
        object Get(string key);
        IDictionary<string, object> GetAll(IEnumerable<string> keys);
        void RemoveCacheAll();
        void RemoveCacheRegex(string pattern);
        IList<string> SearchCacheRegex(string pattern);
        List<string> GetCacheKeys();
    }
}
