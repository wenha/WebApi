using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.ViewModel
{
    /// <summary>
    /// 登录用户信息
    /// </summary>
    public class UserInfo
    {
        public int Id { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 访问权限
        /// </summary>
        public string[] Access { get; set; }

    }
}
