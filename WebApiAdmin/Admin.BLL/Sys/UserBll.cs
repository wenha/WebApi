using Admin.IBLL.Sys;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.BLL.Sys
{
    /// <summary>
    /// 前台用户业务层实现
    /// </summary>
    [Export(typeof(IUserBll))]
    public class UserBll : BaseBll, IUserBll
    {
    }
}
