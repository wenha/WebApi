using Admin.Entity;
using Admin.IDAL.Sys;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.DAL.Sys
{
    /// <summary>
    /// 菜单项目数据层实现
    /// </summary>
    [Export(typeof(IMenuDal))]
    public class MenuDal : BaseDal<SysMenu>, IMenuDal
    {
    }
}
