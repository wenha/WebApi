using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.WebApi.Areas.Admin.Models
{
    /// <summary>
    /// 登录
    /// </summary>
    public class AdminLoginInput
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }
    }
}