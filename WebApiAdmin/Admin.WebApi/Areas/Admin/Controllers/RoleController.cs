using Admin.IBLL.Sys;
using Admin.ViewModel.Sys;
using Admin.WebApi.Areas.Admin.Models.Sys;
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
    /// 角色管理
    /// </summary>
    public class RoleController : AdminBaseController
    {
        #region 公共类

        [Import(typeof(IRoleBll))]
        protected Lazy<IRoleBll> RoleBll { get; set; }

        #endregion

        /// <summary>
        /// 获取角色分页
        /// </summary>
        /// <param name="where"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiAuthor(Name = "wenha", Status = DevStatus.Finish, Time = "2019-07-03")]
        public ApiReturnData<ReturnPagging<RoleOutput>> GetRolePagging(VmRole where, int page = 1, int pageSize = 20)
        {
            var qp = GetQuerryPagging(new string[] { "Id" }, new string[] { "desc" });

            var data = RoleBll.Value.GetRolePagging(qp, where);

            var res = GetReturnPagging(data, a => new RoleOutput
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description
            });
            return res;
        }

        public ApiReturnData<List<RoleOutput>> GetAllRole()
        {

        }
    }
}
