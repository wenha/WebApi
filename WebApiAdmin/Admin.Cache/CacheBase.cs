﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WC = System.Web.Caching;

namespace Admin.Cache
{
    public abstract class CacheBase :ICache
    {
        protected CacheBase()
        {

        }

        /// <summary>
        /// 获取缓存值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        public abstract T Get<T>(string key) where T : class ;

        /// <summary>
        /// 移除缓存，并返回移除的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        public abstract T Remove<T>(string key) where T : class;

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
        public abstract bool Set<T>(string key, T value, DateTime absoluteExpiration, TimeSpan slidingExpiration, WC.CacheItemRemovedCallback onRemoveCallback) where T : class;

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public abstract bool Contains(string key);

        /// <summary>
        /// 设置缓存，不过期
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool Set<T>(string key, T value) where T : class
        {
            return Set(key, value, WC.Cache.NoAbsoluteExpiration, WC.Cache.NoSlidingExpiration, null);
        }


        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="absoluteExpiration">
        /// 所插入对象将到期并被从缓存中移除的时间。
        /// </param>
        /// <param name="onRemoveCallback">在从缓存中移除对象时将调用的委托（如果提供）。 当从缓存中删除应用程序的对象时，可使用它来通知应用程序。</param>
        /// <returns></returns>
        [Obsolete("此方法 onRemoveCallback参数 暂未开放使用")]
        public bool Set<T>(string key, T value, DateTime absoluteExpiration, WC.CacheItemRemovedCallback onRemoveCallback) where T : class
        {
            return Set(key, value, absoluteExpiration, WC.Cache.NoSlidingExpiration, onRemoveCallback);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="slidingExpiration">
        /// 最后一次访问所插入对象时与该对象到期时之间的时间间隔。 
        /// 如果该值等效于 20 分钟，则对象在最后一次被访问 20 分钟之后将到期并被从缓存中移除。 
        /// </param>
        /// <param name="onRemoveCallback">在从缓存中移除对象时将调用的委托（如果提供）。 当从缓存中删除应用程序的对象时，可使用它来通知应用程序。</param>
        /// <returns></returns>
        [Obsolete("此方法 onRemoveCallback参数 暂未开放使用")]
        public bool Set<T>(string key, T value, TimeSpan slidingExpiration, WC.CacheItemRemovedCallback onRemoveCallback) where T : class
        {
            return Set(key, value, WC.Cache.NoAbsoluteExpiration, slidingExpiration, onRemoveCallback);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="absoluteExpiration">
        /// 所插入对象将到期并被从缓存中移除的时间。
        /// </param>
        /// <returns></returns>
        public bool Set<T>(string key, T value, DateTime absoluteExpiration) where T : class
        {
            return Set(key, value, absoluteExpiration, WC.Cache.NoSlidingExpiration, null);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="slidingExpiration">
        /// 最后一次访问所插入对象时与该对象到期时之间的时间间隔。 
        /// 如果该值等效于 20 分钟，则对象在最后一次被访问 20 分钟之后将到期并被从缓存中移除。 
        /// </param>
        /// <returns></returns>
        public bool Set<T>(string key, T value, TimeSpan slidingExpiration) where T : class
        {
            return Set(key, value, WC.Cache.NoAbsoluteExpiration, slidingExpiration, null);
        }
    }
}
