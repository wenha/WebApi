using Admin.Entity;
using Admin.IDAL;
using Admin.IDAL.Sys;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.DAL.Sys
{
    /// <summary>
    /// 前台用户数据层实现
    /// </summary>
    [Export(typeof(IUserDal))]
    public class UserDal : BaseDal<SysUser>, IUserDal
    {
    }
}
