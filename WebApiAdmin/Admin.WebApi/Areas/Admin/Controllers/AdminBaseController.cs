﻿using Admin.ViewModel;
using Admin.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.WebApi.Areas.Admin.Controllers
{
    /// <summary>
    /// 后台控制器基类
    /// </summary>
    public abstract class AdminBaseController : BaseController
    {
        public override UserInfo LoginUser
        {
            get => base.LoginUser;
            protected set
            {
                base.LoginUser = value;
                Session["LoginAdmin"] = value;
            }
        }

        protected AdminBaseController()
        {
            base.LoginUser = Session["LoginAdmin"] as UserInfo;
        }
    }
}