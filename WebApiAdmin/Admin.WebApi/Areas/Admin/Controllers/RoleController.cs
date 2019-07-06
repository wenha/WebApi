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

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiAuthor(Name = "wenha", Status = DevStatus.Finish, Time = "2019-07-04")]
        public ApiReturnData<List<RoleOutput>> GetAllRole()
        {
            var resData = new ApiReturnData<List<RoleOutput>>();

            var data = RoleBll.Value.GetAllRole();

            resData.Data = (from r in data
                            select new RoleOutput
                            {
                                Id = r.Id,
                                Name = r.Name,
                                Description = r.Description
                            }).ToList();

            return SetMessage(resData);
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        [ApiAuthor(Name = "wenha", Status = DevStatus.Finish, Time = "2019-07-04")]
        public ApiReturnData<RoleOutput> GetRoleById(int roleId)
        {
            var resData = new ApiReturnData<RoleOutput>();

            var data = RoleBll.Value.GetRoleById(roleId);

            resData.Data = new RoleOutput()
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description
            };

            return SetMessage(resData);
        }

        /// <summary>
        /// 保存角色
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiAuthor(Name = "wenha", Status = DevStatus.Finish, Time = "2019-07-04")]
        public ApiReturnData<bool> SaveRole([FromBody]VmRole entity)
        {
            var resData = new ApiReturnData<bool>();

            var res = RoleBll.Value.SaveRole(entity, out string code);

            resData.Data = res;
            resData.IsSuccess = res;
            resData.Code = code;

            return SetMessage(resData);
        }

        /// <summary>
        /// 根据Id删除角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        [ApiAuthor(Name = "wenha", Status = DevStatus.Finish, Time = "2019-07-04")]
        public ApiReturnData<bool> DeleteRole(int roleId)
        {
            var resData = new ApiReturnData<bool>();

            var data = RoleBll.Value.DeleteRole(roleId, out string code);

            resData.Data = data;
            resData.Code = code;
            resData.IsSuccess = data;

            return SetMessage(resData);
        }

        /// <summary>
        /// 获取树状组织机构列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        [ApiAuthor(Name = "wenha", Status = DevStatus.Finish, Time = "2019-07-04")]
        public ApiReturnData<List<Tree>> GetTreeMenus(int roleId)
        {
            var resData = new ApiReturnData<List<Tree>>();

            var role = RoleBll.Value.GetRoleById(roleId);

            var roleMenus = RoleBll.Value.GetRoleMenu(roleId).ToArray();
            var allArray = RoleBll.Value.GetAllMenu();

            var treeData = allArray.Select(a => new Tree()
            {
                id = a.Id,
                pid = a.ParentId,
                title = a.Name,
                expand = true,
                @checked = roleMenus.Any(b => a.Id == b.Id),
                code = a.Code,
                level = a.Level
            }).ToList();

            foreach (var tree in treeData)
            {
                tree.children = treeData.Where(a => a.pid == tree.id).ToArray();
                if (tree.children.Count() > 0)
                {
                    tree.@checked = false;
                }
            }

            treeData = treeData.Where(a => a.pid == 0).ToList();

            resData.Data = treeData;

            return SetMessage(resData);
        }

        /// <summary>
        /// 保存访问配置
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpGet]
        [ApiAuthor(Name = "wenha", Status = DevStatus.Finish, Time = "2019-07-04")]
        public ApiReturnData<bool> SaveTreeMenu(int roleId, string ids)
        {
            var resData = new ApiReturnData<bool>();

            var idArray = ids.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(a => Convert.ToInt32(a)).ToArray();
            var result = RoleBll.Value.SaveRoleMenu(roleId, idArray, out string code);

            resData.Data = result;
            resData.IsSuccess = result;

            resData.Code = code;

            return SetMessage(resData);
        }
    }
}
