using System;
using System.Collections.Generic;
using System.Text;

namespace Howell.Math.Algorithm
{
    /// <summary>
    /// Double 运算类
    /// </summary>
    public static class DoubleAlgorithm
    {
        /// <summary>
        /// 表示大于零的最小正 System.Double 值。此字段为常数
        /// </summary>
        public const double Epsilon = 1.0e-10;
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="value1">数值1</param>
        /// <param name="value2">数值2</param>
        /// <returns>如果相同返回true,否则返回false.</returns>
        public static Boolean Equals(Double value1, Double value2)
        {
            return Equals(value1, value2, Epsilon);
        }
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="value1">数值1</param>
        /// <param name="value2">数值2</param>
        /// <param name="ep">表示大于零的最小正 System.Double 值</param>
        /// <returns>如果相同返回true,否则返回false.</returns>
        public static Boolean Equals(Double value1, Double value2, Double ep)
        {
            return (System.Math.Abs(value1 - value2) <=  ep);
        }
    }
}
