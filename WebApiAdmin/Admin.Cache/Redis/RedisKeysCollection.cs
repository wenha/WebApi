using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Cache.Redis
{
    public class RedisKeysCollection : NameObjectCollectionBase
    {
        internal void Add(string key, object value)
        {
            BaseSet(key, value);
        }
    }
}
