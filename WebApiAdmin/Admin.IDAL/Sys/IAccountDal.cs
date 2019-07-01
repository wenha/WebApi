using Admin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.IDAL.Sys
{
    /// <summary>
    /// 后台用户数据层接口
    /// </summary>
    public interface IAccountDal : IBaseDal<SysAccount>
    {
    }
}
