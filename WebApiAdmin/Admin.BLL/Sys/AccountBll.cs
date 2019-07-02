using Admin.Entity;
using Admin.IBLL.Sys;
using Admin.IDAL.Sys;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.BLL.Sys
{
    /// <summary>
    /// 后台用户业务层实现
    /// </summary>
    [Export(typeof(IAccountBll))]
    public class AccountBll : BaseBll, IAccountBll
    {
        #region 公共对象

        [Import(typeof(IAccountDal))]
        protected Lazy<IAccountDal> AccountDal { get; set; }

        #endregion

        /// <summary>
        /// 根据登录名获取后台用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SysAccount GetAccountByName(string name)
        {
            return AccountDal.Value.GetQueryable().FirstOrDefault(a => a.LoginName == name);
        }
    }
}
