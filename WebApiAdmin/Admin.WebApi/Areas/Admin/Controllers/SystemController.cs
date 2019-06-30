﻿using Admin.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Admin.WebApi.Areas.Admin.Controllers
{
    /// <summary>
    /// 系统设置管理
    /// </summary>
    public class SystemController :AdminBaseController
    {
        /// <summary>
        /// 获取所有的角色
        /// </summary>
        /// <returns></returns>
        public ApiReturnData<List<string>> GetAllRole()
        {
            var resData = new ApiReturnData<List<string>>();

            resData.Data = new List<string>() { "admin", "User" };

            return SetMessage(resData);
        }
    }
}
