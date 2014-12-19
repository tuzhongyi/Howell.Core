using System;
using System.Collections.Generic;
using System.Text;
using Howell.Drawing.D2;

namespace Howell.Math.Algorithm
{
    /// <summary>
    /// 平面几何算法
    /// </summary>
    public static class Geometry2DAlgorithm
    {
        /// <summary>
        /// 计算点 (0,0),P,Q,P+Q组成的平行四边形的带符号的面积
        /// P*Q
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="Q">点Q</param>
        /// <returns>返回面积值</returns>
        /// <remarks>
        /// 返回值 大于 0 , 则P在Q的顺时针方向。
        /// 返回值 小于 0 , 则P在Q的逆时针方向。
        /// 返回值 等于 0 , 则P与Q共线，但可能同向也可能反向。
        /// </remarks>
        public static Double Multiple(PointD P, PointD Q)
        {
            //formula
            //则矢量叉积定义为由(0,0)、P、Q和P+Q所组成的平行四边形的带符号的面积，即：P * Q = x1*y2 - x2*y1，其结果是一个标量
            //若 P * Q > 0 , 则P在Q的顺时针方向。
            //若 P * Q < 0 , 则P在Q的逆时针方向。
            //若 P * Q = 0 , 则P与Q共线，但可能同向也可能反向。
            return (P.X * Q.Y - Q.X * P.Y);
        }
        /// <summary>
        /// 计算点P-Q的值
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="Q">点Q</param>
        /// <returns>返回P-Q的值</returns>
        public static PointD Substract(PointD P, PointD Q)
        {
            return new PointD(P.X - Q.X, P.Y - Q.Y);
        }
    }
}
