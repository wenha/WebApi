using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.WebApi.Models
{
    /// <summary>
    /// 方法返回结果类
    /// </summary>
    public class ApiReturnData
    {
        public ApiReturnData()
        {
            IsSuccess = true;
            Code = "0";
        }

        /// <summary>
        /// 访问服务器方法是否成功
        /// 只表示方法是否报错，不用于入库是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string Msg { get; set; }
    }

    /// <summary>
    /// 方法返回结果类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiReturnData<T> : ApiReturnData
    {
        /// <summary>
        /// 返回结果数据
        /// </summary>
        public T Data { get; set; }
    }

    /// <summary>
    /// 方法返回分页数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReturnPagging<T> : ApiReturnData
    { 
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
        public int Total { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int PageCount => PageSize == 0 ? 0 : (int)(Math.Floor((Total - 1) * 1.0 / PageSize) + 1);

        /// <summary>
        /// 分页数据
        /// </summary>
        public T[] Data { get; set; }
    }
}