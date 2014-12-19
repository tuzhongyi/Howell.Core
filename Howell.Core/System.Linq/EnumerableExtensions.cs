using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace System.Linq
{    
    /// <summary>
    /// Enumerable extension functions.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class EnumerableExtensions 
    {
        /// <summary>
        /// 从 System.Collections.Generic.IEnumerable 创建一个 System.Collections.Generic.List 并将类型转换为 TResult。
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TResult">目标类型</typeparam>
        /// <param name="source">源枚举器</param>
        /// <param name="converter">转换器</param>
        /// <returns>返回新的IList对象</returns>
        public static IList<TResult> ToList<TSource,TResult>(this IEnumerable<TSource> source,Func<TSource,TResult> converter)
        {
            IList<TResult> result = new List<TResult>();
            foreach (var item in source)
            {
                result.Add(converter(item));
            }
            return result;
        }
        /// <summary>
        /// 从 System.Collections.Generic.IEnumerable 创建一个 Array 数组， 并将类型转换为 TResult。
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TResult">目标类型</typeparam>
        /// <param name="source">源枚举器</param>
        /// <param name="converter">转换器</param>
        /// <returns></returns>
        public static TResult [] ToArray<TSource,TResult>(this IEnumerable<TSource> source,Func<TSource,TResult> converter)
        {
            IList<TResult> result = new List<TResult>();
            foreach (var item in source)
            {
                result.Add(converter(item));
            }
            return result.ToArray();
        }
        /// <summary>
        /// 从 System.Collections.Generic.IEnumerable 创建一个分页的 System.Collections.Generic.IEnumerable。
        /// </summary>
        /// <param name="source">源枚举器</param>
        /// <param name="pageNo">页码从1开始计数的正整数,</param>
        /// <param name="pageSize">页大小，必须大于0.</param>
        /// <returns></returns>
        public static IEnumerable<TSource> Page<TSource>(this IEnumerable<TSource> source,Int32 pageNo,Int32 pageSize)
        {
            if(pageNo < 1)
                throw new ArgumentOutOfRangeException("pageNo","PageNo must be greater than 0");
            if(pageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize","PageSize must be greater than 0");
            return source.Where((x, index) => ((index >= (pageNo - 1) * pageSize) && (index < (pageNo * pageSize))));
        }
        /// <summary>
        /// 从 System.Collections.Generic.IEnumerable 根据指定分页大小，可以分页
        /// </summary>
        /// <param name="source">源枚举器</param>
        /// <param name="pageSize">分页最大记录条数</param>
        /// <returns>返回分页数目。</returns>
        public static Int32 PageCount<TSource>(this IEnumerable<TSource> source, Int32 pageSize)
        {
            return Convert.ToInt32(System.Math.Ceiling((double)source.Count() / (double)pageSize));
        }

    }
}
