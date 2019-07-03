using Admin.Cache;
using Admin.Cache.Redis;
using Admin.ViewModel;
using Admin.WebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.Http;

namespace Admin.WebApi.Controllers
{
    /// <summary>
    /// Api控制器基类
    /// </summary>
    [ApiError]
    public abstract class BaseController : ApiController
    {
        protected BaseController()
        {
            CreateSession();
            Compose();
        }

        #region 用户信息

        private UserInfo _loginUser { get; set; }

        /// <summary>
        /// 当前登录用户信息
        /// </summary>
        public virtual UserInfo LoginUser
        {
            get => _loginUser;
            protected set => _loginUser = value;
        }

        #endregion

        #region Session

        /// <summary>
        /// 会话缓存
        /// </summary>
        public HttpSessionStateBase Session { get; private set; }

        /// <summary>
        /// 会话超时时间
        /// </summary>
        protected virtual int SessionTimeout { get; set; } = 60 * 24 * 7;

        /// <summary>
        /// 会话id
        /// </summary>
        protected string SessionId { get; private set; }

        /// <summary>
        /// 创建会话
        /// </summary>
        private void CreateSession()
        {
            SessionId = Guid.NewGuid().ToString();
            string token = System.Web.HttpContext.Current.Request.Headers["TOKEN"];
            if (!string.IsNullOrWhiteSpace(token))
            {
                SessionId = token;
            }

            Session = new RedisSession(RedisDB, SessionId);
            Session.Timeout = SessionTimeout; //60 * 24 * 7; //一星期过期时间
        }

        #endregion

        #region Cache

        /// <summary>
        /// 缓存数据库
        /// </summary>
        protected int RedisDB { get; } = Convert.ToInt32(ConfigurationManager.AppSettings["RedisDB"]);

        private ICache _cache;

        public ICache Cache
        {
            get { return _cache ?? (_cache = new Redis(RedisDB)); }
        }

        #endregion


        #region 提示信息
        private static ResourceManager _resourceManager;

        private static ResourceManager ResourceManager
        {
            get
            {
                if (_resourceManager == null)
                {
                    _resourceManager = new ResourceManager("Admin.WebApi.App_LocalResources.Message",
                        Assembly.GetExecutingAssembly());
                }
                return _resourceManager;
            }
        }

        /// <summary>
        /// 获取返回码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetMessage(string key)
        {
            return ResourceManager.GetString(key);
        }


        /// <summary>
        /// 设置返回码
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static ApiReturnData SetMessage(ApiReturnData result)
        {
            result.Msg = GetMessage(result.Code);
            return result;
        }

        /// <summary>
        /// 设置返回码
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static ApiReturnData<T> SetMessage<T>(ApiReturnData<T> result)
        {
            result.Msg = GetMessage(result.Code);
            return result;
        }

        #endregion

        /// <summary>
        /// 获取分页 --带搜索条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="pagging"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        protected ApiReturnData<ReturnPagging<TR>> GetReturnPagging<T, TR>(Pagging<T> pagging, Func<T, TR> selector)
           where T : class
           where TR : class
        {
            ReturnPagging<TR> returnPagging = null;
            if (pagging != null)
            {
                returnPagging = new ReturnPagging<TR>()
                {
                    CurrentPage = pagging.CurrentPage,
                    PageSize = pagging.PageSize,
                    Total = pagging.Total,
                    Data = pagging.Data.Select(selector).ToArray()
                };
            }

            return SetMessage(new ApiReturnData<ReturnPagging<TR>>()
            {
                IsSuccess = true,
                Code = "0",
                Data = returnPagging
            });
        }

        /// <summary>
        /// 获取分页-不带搜索条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pagging"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        protected ApiReturnData<ReturnPagging<T>> GetReturnPagging<T>(Pagging<T> pagging, string code = "0")
            where T : class
        {
            ReturnPagging<T> returnPagging = null;
            if (pagging != null)
            {
                returnPagging = new ReturnPagging<T>()
                {
                    CurrentPage = pagging.CurrentPage,
                    PageSize = pagging.PageSize,
                    Total = pagging.Total,
                    Data = pagging.Data
                };
            }

            return SetMessage(new ApiReturnData<ReturnPagging<T>>()
            {
                IsSuccess = code == "0",
                Code = code,
                Data = returnPagging
            });
        }

        protected void Compose()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.Load("ETL.EightPersonnelCloudEdu.BLL")));
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.Load("ETL.EightPersonnelCloudEdu.DAL")));
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        /// <summary>
        /// 从请求中获取分页对象
        /// page【页码】
        /// pageSize【页大小】
        /// sort【指定排序字段（选填），需要填写order参数，使用半角逗号分隔】
        /// order【指定排序方式（选填），与sort对应，使用半角逗号分隔】
        /// </summary>
        /// <param name="defaultSortName">默认排序</param>
        /// <param name="defaultSortOrder"></param>
        /// <returns></returns>
        protected virtual QueryPagging GetQuerryPagging(string[] defaultSortName, string[] defaultSortOrder)
        {
            var request = HttpContext.Current.Request;
            var pagging = new QueryPagging();
            int page, pageSize;
            if (!int.TryParse(request["page"], out page) || page <= 0)
            {
                page = 1;
            }
            if (!int.TryParse(request["pageSize"], out pageSize) || pageSize <= 0)
            {
                pageSize = 20;
            }
            string sort = request["sort"];
            string order = request["order"];
            if (!string.IsNullOrWhiteSpace(sort))
            {
                pagging.SortName = sort.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }
            if (!string.IsNullOrWhiteSpace(order))
            {
                pagging.SortOrder = order.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }
            pagging.DefaultSortName = defaultSortName;
            pagging.DefaultSortOrder = defaultSortOrder;
            pagging.Page = page;
            pagging.PageSize = pageSize;
            return pagging;
        }
    }
}
