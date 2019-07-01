using Admin.IBLL.Sys;
using Admin.WebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Admin.WebApi.Areas.Admin.Controllers
{
    /// <summary>
    /// 后台用户管理
    /// </summary>
    public class AccountController : AdminBaseController
    {
        #region 公共类

        [Export(typeof(IAccountBll))]
        protected Lazy<IAccountBll> AccountBll { get; set; }

        #endregion

    }
}
