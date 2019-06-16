using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace Admin.Cache.Base
{
    public class RedisSession : HttpSessionStateBase
    {
        private readonly IDatabase _db;

        private readonly bool _isNewSession;
        private readonly string _sessinId;

        private readonly object _syncRoot = new object();
        private int _timeout = 20;

        private string SessionKey
        {
            get { return "_S:" + _sessinId; }
        }

        public RedisSession(int db, string sessinId)
        {
            _sessinId = sessinId;
            _db = Redis.Connection.GetDatabase(db);
            _isNewSession = !_db.KeyExists(SessionKey);
            if (!_isNewSession)
            {
                SetExpire();
            }
            this.LCID = db;
        }

        public override SessionStateMode Mode
        {
            get { return SessionStateMode.Custom; }
        }

        /// <summary>
        /// 过期时间
        /// </summary>
        public override int Timeout
        {
            get { return _timeout; }
            set
            {
                _timeout = value;
                SetExpire();
            }
        }

        /// <summary>
        /// 会话ID
        /// </summary>
        public override string SessionID
        {
            get { return _sessinId; }
        }

        public override bool IsNewSession
        {
            get { return _isNewSession; }
        }


        /// <summary>
        /// 添加会话
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public override void Add(string name, object value)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "键为空");
            }

            if (value == null)
            {
                Remove(name);
                return;
            }
            string strSpl = "[$&$]";
            var type = value.GetType();
            string strType;
            using (MemoryStream ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, type);
                ms.Seek(0L, SeekOrigin.Begin);
                var bytes = ms.GetBuffer();
                strType = Convert.ToBase64String(bytes);
            }
            _db.HashSet(SessionKey, name, JsonConvert.SerializeObject(value) + strSpl + strType);
            SetExpire();
        }

        /// <summary>
        /// 设置过期时间
        /// </summary>
        private void SetExpire()
        {
            var a = _db.KeyExpire(SessionKey, new TimeSpan(0, Timeout, 0));
        }

        /// <summary>
        /// 获取会话值
        /// </summary>
        public override object this[string name]
        {
            get
            {
                if (!_db.HashExists(SessionKey, name))
                {
                    return null;
                    throw new ArgumentOutOfRangeException(name);
                }
                var strValue = _db.HashGet(SessionKey, name).ToString();
                var values = strValue.Split(new[] { "[$&$]" }, StringSplitOptions.RemoveEmptyEntries);
                using (var ms = new MemoryStream(Convert.FromBase64String(values[1])))
                {
                    var type = (Type)new BinaryFormatter().Deserialize(ms);
                    return JsonConvert.DeserializeObject(values[0], type);
                }
            }
            set { Add(name, value); }
        }

        public override object this[int index]
        {
            get { return base[index]; }
            set { base[index] = value; }
        }

        public override int CodePage { get; set; }

        public override HttpSessionStateBase Contents
        {
            get { return this; }
        }

        public override HttpCookieMode CookieMode
        {
            get { return HttpCookieMode.UseCookies; }
        }
        /// <summary>
        /// 会话键的数量
        /// </summary>
        public override int Count
        {
            get { return (int)_db.HashLength(SessionKey); }
        }

        public override bool IsCookieless
        {
            get { return true; }
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public override bool IsSynchronized
        {
            get { return false; }
        }

        /// <summary>
        /// 会话所有键
        /// </summary>
        public override NameObjectCollectionBase.KeysCollection Keys
        {
            get
            {
                var keys = new RedisKeyCollection();
                var all = _db.HashKeys(SessionKey);
                SetExpire();
                foreach (var item in all)
                {
                    keys.Add(item.ToString(), null);
                }
                return keys.Keys;
            }
        }

        public override int LCID { get; set; }


        public override HttpStaticObjectsCollectionBase StaticObjects
        {
            get { return base.StaticObjects; }
        }

        public override object SyncRoot
        {
            get { return _syncRoot; }
        }

        /// <summary>
        /// 清空键值
        /// </summary>
        public override void Clear()
        {
            _db.KeyDelete(SessionKey);
        }

        /// <summary>
        /// 移除键值
        /// </summary>
        /// <param name="name"></param>
        public override void Remove(string name)
        {
            _db.HashDelete(SessionKey, name);
        }

        public override void RemoveAll()
        {
            Clear();
        }

        public override void RemoveAt(int index)
        {
            base.RemoveAt(index);
        }

        public override void Abandon()
        {
            Clear();
        }
    }
}