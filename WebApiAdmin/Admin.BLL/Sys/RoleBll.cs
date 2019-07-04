using Admin.Entity;
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

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        public List<VmRole> GetAllRole()
        {
            var dmRoles = RoleDal.Value.GetQueryable().Where(r => !r.IsDelete);

            var vmRoles = (from r in dmRoles
                           select new VmRole
                           {
                               Id = r.Id,
                               Name = r.Name,
                               Description = r.Description
                           }).ToList();

            return vmRoles;
        }

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public VmRole GetRoleById(int roleId)
        {
            var dmRole = RoleDal.Value.GetQueryable().FirstOrDefault(r => r.Id == roleId);

            var role = new VmRole()
            {
                Id = dmRole.Id,
                Name = dmRole.Name,
                Description = dmRole.Description
            };

            return role;
        }

        /// <summary>
        /// 保存角色
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool SaveRole(VmRole entity, out string code)
        {
            code = "OK";

            var role = new SysRole();

            role.na
        }

        /// <summary>
        /// 根据Id删除角色
        /// </summary>
        /// <paraame="roleId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool DeleteRole(int roleId, out string code)
        {
            code = "OK";

            var role = RoleDal.Value.GetQueryable().FirstOrDefault(r => r.Id == roleId);

            role.IsDelete = true;

            RoleDal.Value.Update(role);
            SaveChanges();

            return true;
        }

        /// <summary>
        /// 获取所有后台所有菜单
        /// </summary>
        /// <returns></returns>
        public List<SysMenu> GetAllMenu()
        {

        }

        /// <summary>
        /// 保存角色菜单权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menus"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool SaveRoleMenu(int roleId, int[] menus, out string code)
        {

        }
    }
}

 