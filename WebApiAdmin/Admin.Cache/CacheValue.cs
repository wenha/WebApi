using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Cache
{
    internal class CacheValue
    {
        /// <summary>
        /// 过期时间（相对时间）
        /// </summary>
        public TimeSpan SlidingExpiration { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string JsonValue { get; set; }
    }
}
