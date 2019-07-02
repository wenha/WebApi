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
    /// 组织机构业务层实现
    /// </summary>
    [Export(typeof(IOrganBll))]
    public class OrganBll : BaseBll, IOrganBll
    {
    }
}
