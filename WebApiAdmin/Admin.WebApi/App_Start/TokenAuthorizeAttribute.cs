using Admin.WebApi.Controllers;
using Admin.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Admin.WebApi
{
    /// <summary>
    /// Token认证
    /// </summary>
    public class TokenAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 是否认证
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (actionContext.ControllerContext.Controller is BaseController baseApi)
            {
                if (actionContext.Request.Headers.TryGetValues("TOKEN", out var tokens)
                    && tokens.Any() && !string.IsNullOrEmpty(tokens.ToArray()[0]))
                {
                    var token = tokens.ToArray()[0];
                    var app = baseApi.Cache.Get<string>($"token_{token}");
                    if (app == null)
                    {
                        return false;
                    }
                    Token = token;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 处理未授权请求
        /// </summary>
        /// <param name="actionContext"></param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);

            var content = new ApiReturnData
            {
                Code = "10009",
                Msg = "Invalid token"
            };
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, content);
            if (actionContext.ControllerContext.Controller is BaseController baseApi)
            {
                if (baseApi.LoginUser == null)
                {
                    actionContext.Response.Headers.Add("SESSIONSTATUS", "TIMEOUT");
                }
            }
        }
    }
}