using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.ViewModel
{
    /// <summary>
    /// 分页对象
    /// </summary>
    public class Pagging
    {

        /// <summary>
        /// 开始位置
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// 结束位置
        /// </summary>
        public int End { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage { get; set; }


        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; protected set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int PageCount => PageSize == 0 ? 0 : (int)(Math.Floor((Total - 1) * 1.0 / PageSize) + 1);
    }

    /// <summary>
    /// 分页对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pagging<T> : Pagging, IEnumerable<T> where T : class
    {

        private QueryPagging QueryPagging { get; }

        public T[] Data { get; }

        public Pagging(QueryPagging pagging, T[] data, int total)
        {
            QueryPagging = pagging;
            Start = QueryPagging.Start;
            End = QueryPagging.End;
            PageSize = QueryPagging.PageSize;
            Start = QueryPagging.Start;
            CurrentPage = QueryPagging.Page;
            Data = data;
            Total = total;
        }

        public string[] SortName => QueryPagging.SortName;

        public string[] SortOrder => QueryPagging.SortOrder;

        public string[] DefaultSortName => QueryPagging.DefaultSortName;

        public string[] DefaultSortOrder => QueryPagging.DefaultSortOrder;

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T data in Data)
            {
                yield return data;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Data.GetEnumerator();
        }
    }

    /// <summary>
    /// 查询分页对象
    /// </summary>
    public class QueryPagging
    {
        /// <summary>
        /// 查询的页数
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string[] SortName { get; set; }
        /// <summary>
        /// 排序方式
        /// </summary>
        public string[] SortOrder { get; set; }
        /// <summary>
        /// 默认排序字段
        /// </summary>
        public string[] DefaultSortName { get; set; }
        /// <summary>
        /// 默认排序方式
        /// </summary>
        public string[] DefaultSortOrder { get; set; }

        /// <summary>
        /// 开始位置
        /// </summary>
        public int Start => (Page - 1) * PageSize;

        /// <summary>
        /// 结束位置
        /// </summary>
        public int End => Page * PageSize;
    }
}
