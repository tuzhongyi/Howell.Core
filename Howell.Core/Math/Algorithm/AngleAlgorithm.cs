using System;
using System.Collections.Generic;
using System.Text;

namespace Howell.Math.Algorithm
{
    /// <summary>
    /// 角度计算类
    /// </summary>
    public static class AngleAlgorithm
    {

        /// <summary>
        /// 角度转换弧度
        /// </summary>
        /// <param name="degrees">角度</param>
        /// <returns>返回弧度值</returns>
        public static Double ToRadians(Double degrees)
        {
            return (System.Math.PI / 180) * degrees;
        }
        /// <summary>
        /// 弧度转换角度
        /// </summary>
        /// <param name="radians">弧度</param>
        /// <returns>返回角度值</returns>
        public static Double ToDegrees(Double radians)
        {
            return ((radians * 180) / System.Math.PI);
        }

    }
}
