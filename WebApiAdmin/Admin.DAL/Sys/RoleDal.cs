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
    [Export(typeof(IRoleDal))]
    public class RoleDal : BaseDal<SysRole>, IRoleDal
    {
    }
}
