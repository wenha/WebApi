using Admin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Admin.WebApi.Controllers
{
    /// <summary>
    /// 前台用户控制器基类
    /// </summary>
    public abstract class UserBaseController : BaseController
    {
        public override UserInfo LoginUser
        {
            get => base.LoginUser;
            protected set
            {
                base.LoginUser = value;
                Session["LoginUser"] = value;
            }
        }

        protected UserBaseController()
        {
            base.LoginUser = Session["LoginUser"] as UserInfo;
        }
    }
}
