using System;
using System.Collections.Generic;
using System.Text;

namespace Howell.Math.Algorithm
{
    /// <summary>
    /// 最大值,最小值计算类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MinMax<T> 
        where T : IComparable , IComparable<T>
    {
        private T m_Min;
        private T m_Max;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="a">数值a</param>
        /// <param name="b">数值b</param>
        public MinMax(T a, T b)
        {
            if (a.CompareTo(b) > 0)
            {
                m_Max = a;
                m_Min = b;
            }
            else
            {
                m_Min = a;
                m_Max = b;
            }
        }
        /// <summary>
        /// 数值A,B中的最小值
        /// </summary>
        public T Min
        {
            get
            {
                return m_Min;
            }
        }
        /// <summary>
        /// 数值A,B中的最大值
        /// </summary>
        public T Max
        {
            get
            {
                return m_Max;
            }
        }
        /// <summary>
        /// 是否在范围内
        /// </summary>
        /// <param name="value">数值</param>
        /// <returns>返回True表示在范围内</returns>
        public Boolean InRange(T value)
        {
            return ((Min.CompareTo(value) <= 0) && (Max.CompareTo(value) >= 0));
        }
    }

}
