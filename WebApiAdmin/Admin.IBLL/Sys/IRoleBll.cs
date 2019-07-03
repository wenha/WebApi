using Admin.Entity;
using Admin.ViewModel;
using Admin.ViewModel.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.IBLL.Sys
{
    /// <summary>
    /// 角色业务层接口
    /// </summary>
    public interface IRoleBll
    {
        /// <summary>
        /// 获取角色分页
        /// </summary>
        /// <param name="queryPagging"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        Pagging<VmRole> GetRolePagging(QueryPagging queryPagging, VmRole where);
    }
}
