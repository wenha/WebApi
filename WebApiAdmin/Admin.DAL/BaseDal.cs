using Admin.Entity;
using Admin.IDAL;
using Admin.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Admin.DAL
{
    /// <summary>
    /// 数据访问基类
    /// </summary>
    public abstract class BaseDal : IBaseDal
    {
        private WebApiEntities _dbContext;

        protected BaseDal()
        {
        }

        protected WebApiEntities DbContext
        {
            get
            {
                _dbContext = CallContext.GetData("DbContext") as WebApiEntities;
                if (_dbContext == null)
                {
                    _dbContext = new WebApiEntities();
                    CallContext.SetData("DbContext", _dbContext);
                }
                return _dbContext;
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TM"></typeparam>
        /// <param name="pagging">分页排序、页大小、页码等分页对象</param>
        /// <param name="queryable">要分页的IQueryable对象</param>
        /// <returns></returns>
        protected internal Pagging<TM> Pagging<TM>(QueryPagging pagging, IQueryable<TM> queryable) where TM : class
        {
            var count = queryable.Count();
            var data = Queryable.Take<TM>(OrderedQueryable(queryable, pagging).Skip(pagging.Start), pagging.PageSize).ToArray();
            return new Pagging<TM>(pagging, data, count);
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="TM"></typeparam>
        /// <param name="queryable">要排序的IQueryable对象</param>
        /// <param name="pagging">分页对象</param>
        /// <returns></returns>
        protected IQueryable<TM> OrderedQueryable<TM>(IQueryable<TM> queryable, QueryPagging pagging) where TM : class
        {
            var sortNames = pagging.DefaultSortName;
            var sortOrders = pagging.DefaultSortOrder;
            if (pagging.SortName != null && pagging.SortName.Length > 0)
            {
                sortNames = pagging.SortName;
                sortOrders = pagging.SortOrder;
            }

            var type = typeof(TM);
            for (int i = 0; i < sortNames.Length; i++)
            {
                var j = i;

                var property = type.GetProperty(sortNames[j]);
                var parameter = Expression.Parameter(type, "p");
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                LambdaExpression orderByExp = Expression.Lambda(propertyAccess, parameter);
                MethodCallExpression resultExp;
                if (i == 0)
                {
                    resultExp = sortOrders[i] == "desc"
                        ? Expression.Call(typeof(Queryable), "OrderByDescending", new Type[] { type, property.PropertyType },
                            queryable.Expression, Expression.Quote(orderByExp))
                        : Expression.Call(typeof(Queryable), "OrderBy", new Type[] { type, property.PropertyType },
                            queryable.Expression, Expression.Quote(orderByExp));
                }
                else
                {
                    resultExp = sortOrders[i] == "desc"
                        ? Expression.Call(typeof(Queryable), "ThenByDescending", new Type[] { type, property.PropertyType },
                            queryable.Expression, Expression.Quote(orderByExp))
                        : Expression.Call(typeof(Queryable), "ThenBy", new Type[] { type, property.PropertyType },
                            queryable.Expression, Expression.Quote(orderByExp));
                }
                queryable = queryable.Provider.CreateQuery<TM>(resultExp);
            }
            return queryable;
        }
    }

    public abstract class BaseDal<T> : BaseDal, IBaseDal<T> where T : class
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entry"></param>
        public virtual void Add(T entry)
        {
            DbContext.Entry<T>(entry).State = EntityState.Added;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entry"></param>
        public virtual void Update(T entry)
        {
            DbContext.Entry<T>(entry).State = EntityState.Modified;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entry"></param>
        public virtual void Delete(T entry)
        {
            DbContext.Entry<T>(entry).State = EntityState.Deleted;
        }

        /// <summary>
        /// 获取IQueryable--不分页
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetQueryable()
        {
            return DbContext.Set<T>();
        }

        /// <summary>
        /// 获取IQueryable
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetQueryable(Expression<Func<T, bool>> where)
        {
            return GetQueryable().Where(where);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pagging"></param>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public Pagging<T> GetPagging(QueryPagging pagging, IQueryable<T> queryable)
        {
            return Pagging(pagging, queryable);
        }
    }
}
