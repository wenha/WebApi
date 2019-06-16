using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Admin.WebApi
{
    /// <summary>
    /// 错误处理
    /// </summary>
    public class ApiErrorAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext filterContext)
        {
            base.OnException(filterContext);

#if DEBUG
            var content = new
            {
                Code = "90000",
                IsSuccess = false,
                Msg = "Server Error",
                ExceptionMessage = filterContext.Exception.Message,
                ExceptionType = filterContext.Exception.GetType().ToString(),
                StackTrace = filterContext.Exception.StackTrace,
            };
            filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.InternalServerError, content);
#endif

#if !DEBUG
            var info = Guid.NewGuid().ToString("N");
            var message = new StringBuilder();
            message.AppendLine("-------------------------------------------");
            message.AppendLine("API请求信息：" + info);
            message.AppendLine($"  客户端IP：{HttpContext.Current.Request.UserHostAddress}");
            message.AppendLine($"  访问地址：{HttpContext.Current.Request.Url}");
            message.AppendLine($"  访问凭证：{HttpContext.Current.Request.Headers["TOKEN"]}");
            message.AppendLine($"  访问时间：{HttpContext.Current.Timestamp:yyyy-MM-dd HH:mm:ss.fff}");
            message.AppendLine($"  Controller：{filterContext.ActionContext.ControllerContext.RouteData.Values["controller"]}");
            message.AppendLine($"  Action：{filterContext.ActionContext.ControllerContext.RouteData.Values["action"]}");
            LogHelper.WriteWarnLog(message.ToString(), filterContext.Exception);
#endif
        }
    }
}