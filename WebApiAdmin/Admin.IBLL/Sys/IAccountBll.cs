using Admin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.IBLL.Sys
{
    /// <summary>
    /// 后台用户业务层接口
    /// </summary>
    public interface IAccountBll
    {

        /// <summary>
        /// 根据用户名获取后台用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        SysAccount GetAccountByName(string name);
    }
}
