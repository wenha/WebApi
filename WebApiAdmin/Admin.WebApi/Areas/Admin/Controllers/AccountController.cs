using Admin.Common;
using Admin.IBLL.Sys;
using Admin.ViewModel;
using Admin.WebApi.Areas.Admin.Models;
using Admin.WebApi.Models;
using Swashbuckle.Swagger.Annotations;
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

        [Import(typeof(IAccountBll))]
        protected Lazy<IAccountBll> AccountBll { get; set; }

        #endregion

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ApiAuthor(Name = "wenha", Status = DevStatus.Finish, Time = "2019-07-01")]
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

            var pwd = SecurityHelper.MD5(login.Password);
            if (account.Password != SecurityHelper.MD5(login.Password))
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

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiAuthor(Name = "wenha", Status = DevStatus.Finish, Time = "2019-07-03")]
        public ApiReturnData<bool> LoginOut()
        {
            var resData = new ApiReturnData<bool>();

            LoginUser = null;

            return SetMessage(resData);
        }

        /// <summary>
        /// 获取后台用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiAuthor(Name = "wenha", Status = DevStatus.Finish, Time = "2019-07-04")]
        public ApiReturnData<UserInfo> Info()
        {
            var resData = new ApiReturnData<UserInfo>();

            resData.Data = LoginUser;

            return SetMessage(resData);
        }
    }
}
