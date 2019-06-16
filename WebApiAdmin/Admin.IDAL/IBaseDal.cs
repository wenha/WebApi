using Admin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Admin.IDAL
{
    /// <summary>
    /// 数据访问层基类
    /// </summary>
    public interface IBaseDal
    {
        // void SaveChanges();
    }

    /// <summary>
    /// 数据访问层泛型基类
    /// </summary>
    public interface IBaseDal<T> : IBaseDal where T : class
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entry"></param>
        void Add(T entry);

        /// <summary>
        /// 修改
        /// </summary>
        void Update(T entry);

        /// <summary>
        /// 删除
        /// </summary>
        void Delete(T entry);

        /// <summary>
        /// 获取IQueryable对象-不分页
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetQueryable();

        /// <summary>
        /// 获取IQueryable对象
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IQueryable<T> GetQueryable(Expression<Func<T, bool>> where);

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pagging"></param>
        /// <param name="queryable"></param>
        /// <returns></returns>
        Pagging<T> GetPagging(QueryPagging pagging, IQueryable<T> queryable);
    }
}
