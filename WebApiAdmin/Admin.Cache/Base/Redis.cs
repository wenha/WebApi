using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WC = System.Web.Caching;

namespace Admin.Cache.Base
{
    public class Redis : CacheBase
    {
        internal static ConnectionMultiplexer Connection;
        private readonly IDatabase _db;

        public Redis(int db)
        {
            _db = Connection.GetDatabase(db);
        }

        public static void SetConnection(string connectStr)
        {
            Connection?.Dispose();
            Connection = ConnectionMultiplexer.Connect(connectStr);
        }

        /// <summary>
        /// 获取缓存值，为null时缓存不存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        public override T Get<T>(string key)
        {
            if (!Contains(key))
            {
                return null;
            }
            var value = _db.StringGet(key);
            if (value.IsNullOrEmpty)
            {
                return null;
            }
            var cacheValue = JsonConvert.DeserializeObject(value.ToString(), typeof(CacheValue)) as CacheValue;
            if (cacheValue == null)
            {
                return null;
            }
            if (cacheValue.SlidingExpiration != WC.Cache.NoSlidingExpiration)
            {
                _db.KeyExpire(key, cacheValue.SlidingExpiration);
            }
            return JsonConvert.DeserializeObject(cacheValue.JsonValue, typeof(T)) as T;
        }

        public override T Remove<T>(string key)
        {
            var value = Get<T>(key);
            _db.KeyDelete(key);
            return value;
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="absoluteExpiration">
        /// 所插入对象将到期并被从缓存中移除的时间。
        /// 如果使用绝对到期，则 slidingExpiration 参数必须为 NoSlidingExpiration。
        /// </param>
        /// <param name="slidingExpiration">
        /// 最后一次访问所插入对象时与该对象到期时之间的时间间隔。 
        /// 如果该值等效于 20 分钟，则对象在最后一次被访问 20 分钟之后将到期并被从缓存中移除。 
        /// 如果使用可调到期，则 absoluteExpiration 参数必须为 NoAbsoluteExpiration。
        /// </param>
        /// <param name="onRemoveCallback">在从缓存中移除对象时将调用的委托（如果提供）。 当从缓存中删除应用程序的对象时，可使用它来通知应用程序。</param>
        /// <returns></returns>
        [Obsolete("此方法 onRemoveCallback参数 暂未开放使用")]
        public override bool Set<T>(string key, T value, DateTime absoluteExpiration, TimeSpan slidingExpiration,
            WC.CacheItemRemovedCallback onRemoveCallback)
        {
            TimeSpan? expiry = slidingExpiration;
            if (absoluteExpiration == WC.Cache.NoAbsoluteExpiration
                && slidingExpiration == WC.Cache.NoSlidingExpiration)
            {
                expiry = null;
            }
            var cacheValue = new CacheValue()
            {
                SlidingExpiration = slidingExpiration,
                JsonValue = JsonConvert.SerializeObject(value)
            };
            if (absoluteExpiration != WC.Cache.NoAbsoluteExpiration)
            {
                expiry = absoluteExpiration - DateTime.Now;
            }
            return _db.StringSet(key, JsonConvert.SerializeObject(cacheValue), expiry);
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public override bool Contains(string key)
        {
            return _db.KeyExists(key);
        }
    }
}
