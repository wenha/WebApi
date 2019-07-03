using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.WebApi.Areas.Admin.Models.Sys
{
    /// <summary>
    /// 角色
    /// </summary>
    public class RoleOutput
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description { get; set; }
    }
}