using Admin.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Admin.WebApi.Controllers
{
    /// <summary>
    /// 前台用户管理
    /// </summary>
    public class UserController : UserBaseController
    {
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        public ApiReturnData<bool> Test()
        {
            var resData = new ApiReturnData<bool>();

            return SetMessage(resData);
        }
    }
}
