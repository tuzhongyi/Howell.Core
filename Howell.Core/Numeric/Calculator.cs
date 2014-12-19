using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Howell.Numeric
{
    /// <summary>
    /// 四则运算的计算器对象
    /// </summary>
    /// <typeparam name="T">计算器数值类型</typeparam>
    public class Calculator<T>
    {
        /// <summary>
        /// 默认构造
        /// </summary>
        public Calculator()
        {
        }
        /// <summary>
        /// 加
        /// </summary>
        /// <param name="t1">数1</param>
        /// <param name="t2">数2</param>
        /// <returns></returns>
        public T Add(T t1, T t2);
        public T Subtract(T t1, T t2);
        public T Multiply(T t1, T t2);
        public T Divide(T t1, T t2);
    }
    /// <summary>
    /// Calculator泛型扩展方法
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class CalculatorExtensions
    {
        #region Add
        /// <summary>
        /// 操作数
        /// </summary>
        /// <param name="calc"></param>
        /// <param name="o1"></param>
        /// <param name="o2"></param>
        /// <returns></returns>
        public static Int64 Add(this Calculator<Int64> calc, Int64 o1, Int64 o2)
        {
            return o1 + o2;
        }
        #endregion
    }
    
}
