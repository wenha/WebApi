using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.ViewModel.Sys
{
    /// <summary>
    /// 后台树状权限列表 -- Vue中tree控件
    ///</summary>
    public class Tree
    {
        public int id { get; set; }

        public int? pid { get; set; }

        public string title { get; set; }

        public bool expand { get; set; }

        public bool disabled { get; set; }

        public bool disableCheckbox { get; set; }

        public bool selected { get; set; }

        public bool @checked { get; set; }

        public Tree[] children { get; set; }

        public string code { get; set; }

        public int level { get; set; }
    }
}
