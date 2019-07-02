using Admin.Common;
using Admin.IBLL.Sys;
using Admin.WebApi.Areas.Admin.Models;
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

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ApiReturnData<string> Login([FromBody]AdminLoginInput login)
        {
            var resData = new ApiReturnData<string>();

            var account = AccountBll.Value.GetAccountByName(login.LoginName);

            if(account == null)
            {
                resData.Code = "NotFindAccount";
                resData.Data = "";
                resData.IsSuccess = false;
                return SetMessage(resData);
            }
            if(account.Password != SecurityHelper.MD5(login.Password))
            {
                resData.Code = "WrongPassword";
                resData.Data = "";
                resData.IsSuccess = false;
                return SetMessage(resData);
            }
            LoginUser = new ViewModel.UserInfo()
            {
                Id = account.Id,
                Name = account.Name,
                UserName = account.LoginName,

            };
            resData.Data = SessionId;
            return SetMessage(resData);
        }
    }
}
