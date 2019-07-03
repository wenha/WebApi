using Admin.IBLL.Sys;
using Admin.IDAL.Sys;
using Admin.ViewModel;
using Admin.ViewModel.Sys;
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
        #region 公共类

        [Import(typeof(IRoleDal))]
        protected Lazy<IRoleDal> RoleDal { get; set; }

        #endregion

        /// <summary>
        /// 获取角色分页
        /// </summary>
        /// <param name="queryPagging"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public Pagging<VmRole> GetRolePagging(QueryPagging queryPagging, VmRole where)
        {
            return RoleDal.Value.GetRolePagging(queryPagging, where);
        }
    }
}
