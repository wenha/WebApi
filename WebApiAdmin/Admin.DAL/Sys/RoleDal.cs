using Admin.Entity;
using Admin.IDAL.Sys;
using Admin.ViewModel;
using Admin.ViewModel.Sys;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.DAL.Sys
{
    [Export(typeof(IRoleDal))]
    public class RoleDal : BaseDal<SysRole>, IRoleDal
    {
        /// <summary>
        /// 获取所有角色-分页
        /// </summary>
        /// <param name="queryPagging"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public Pagging<VmRole> GetRolePagging(QueryPagging queryPagging, VmRole where)
        {
            var db = DbContext;

            var dm = (from r in db.SysRole
                      where r.IsDelete == false && r.Name != "Admin"
                      select new VmRole
                      {
                          Id = r.Id,
                          Name = r.Name,
                          Description = r.Description
                      });
            if (where != null)
            {
                if (!string.IsNullOrEmpty(where.Name))
                {
                    dm = dm.Where(r => r.Name.Contains(where.Name));
                }
            }
            return Pagging(queryPagging, dm);
        }
    }
}
