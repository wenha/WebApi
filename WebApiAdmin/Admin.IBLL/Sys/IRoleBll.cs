using Admin.Entity;
using Admin.ViewModel;
using Admin.ViewModel.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.IBLL.Sys
{
    /// <summary>
    /// 角色业务层接口
    /// </summary>
    public interface IRoleBll
    {
        /// <summary>
        /// 获取角色分页
        /// </summary>
        /// <param name="queryPagging"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        Pagging<VmRole> GetRolePagging(QueryPagging queryPagging, VmRole where);

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        List<VmRole> GetAllRole();

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        VmRole GetRoleById(int roleId);

        /// <summary>
        /// 保存角色
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        bool SaveRole(VmRole entity, out string code);

        /// <summary>
        /// 根据Id删除角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        bool DeleteRole(int roleId, out string code);

        /// <summary>
        /// 获取所有后台所有菜单
        /// </summary>
        /// <returns></returns>
        List<SysMenu> GetAllMenu();

        /// <summary>
        /// 保存角色菜单权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menus"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        bool SaveRoleMenu(int roleId, int[] menus, out string code);
    }
}
