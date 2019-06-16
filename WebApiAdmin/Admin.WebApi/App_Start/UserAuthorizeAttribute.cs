using Admin.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;

namespace Admin.WebApi
{
    /// <summary>
    /// 不验证身份
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class AllowUserAttribute : Attribute
    {

    }

    /// <summary>
    /// 登录用户认证
    /// </summary>
    public class UserAuthorizeAttribute : TokenAuthorizeAttribute
    {
        /// <summary>
        /// 是否认证
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var isAllowUser = actionContext.ActionDescriptor
                                  .GetCustomAttributes<AllowUserAttribute>(false).Any()
                              || actionContext.ActionDescriptor.ControllerDescriptor
                                  .GetCustomAttributes<AllowUserAttribute>(false).Any();
            if (actionContext.ControllerContext.Controller is BaseController baseApi)
            {
                isAllowUser = isAllowUser && baseApi.LoginUser != null;
                if (isAllowUser)
                {
                    return true;
                }
                return baseApi.LoginUser != null;
            }
            return true;
        }

    }
}