using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Admin.BLL
{
    /// <summary>
    /// 业务层基类
    /// </summary>
    public abstract class BaseBll
    {
        protected BaseBll()
        {
        }

        /// <summary>
        /// 组合部件
        /// </summary>
        protected void Compose()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.Load("Admin.DAL")));
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
        /// <summary>
        /// 提交数据库
        /// </summary>
        protected void SaveChanges()
        {
            var context = CallContext.GetData("DbContext") as DbContext;
            context?.SaveChanges();
        }


        /// <summary>
        /// 提交数据库并关闭连接
        /// </summary>
        protected void SaveChangesAndClose()
        {
            var context = CallContext.GetData("DbContext") as DbContext;
            context?.SaveChanges();
            context?.Dispose();
        }
    }
}
