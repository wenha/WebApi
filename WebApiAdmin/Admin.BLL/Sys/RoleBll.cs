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
    /// 角色业务层实现
    /// </summary>
    [Export(typeof(IRoleBll))]
    public class RoleBll : BaseBll, IRoleBll
    {
    }
}
