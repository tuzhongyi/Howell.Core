using System;
using System.Collections.Generic;
using System.Text;
using Howell.Drawing.D2;

namespace Howell.Math.Algorithm
{
    /// <summary>
    /// 多线段计算
    /// </summary>
    public static class PolylineAlgorithm
    {
        #region Deflecting Direction 
        /// <summary>
        /// 判断折线的偏转方向/线段拐向
        /// </summary>
        /// <param name="P">P点</param>
        /// <param name="Q">Q点</param>
        /// <param name="R">R点</param>
        /// <returns>返回偏转方向</returns>
        /// <remarks>
        /// 返回值 大于 0 , 则PQ在R点拐向右侧后得到QR,等同于点R在PQ线段的右侧
        /// 返回值 小于 0 , 则PQ在R点拐向左侧后得到QR,等同于点R在PQ线段的左侧
        /// 返回值 等于 0 , 则P,Q,R三点共线。
        /// </remarks>
        public static Double DeflectingDirection(PointD P, PointD Q, PointD R)
        {
            //formular
            //折线段的拐向判断方法可以直接由矢量叉积的性质推出。对于有公共端点的线段PQ和QR，通过计算(R - P) * (Q - P)的符号便可以确定折线段的拐向
            //基本算法是(R-P)计算相对于P点的R点坐标,(Q-P)计算相对于P点的Q点坐标。
            //(R-P) * (Q-P)的计算结果是计算以P为相对原点,点R与点Q的顺时针，逆时针方向。
            PointD RP = PointAlgorithm.Substract(R, P);//R-P
            PointD QP = PointAlgorithm.Substract(Q, P);//Q-P
            return PointAlgorithm.Multiple(RP, QP);
        }
        /// <summary>
        /// 判断折线的偏转方向/线段拐向
        /// </summary>
        /// <param name="P">P点</param>
        /// <param name="Q">Q点</param>
        /// <param name="R">R点</param>
        /// <returns>返回偏转方向</returns>
        /// <remarks>
        /// 返回值 大于 0 , 则PQ在R点拐向右侧后得到QR,等同于点R在PQ线段的右侧
        /// 返回值 小于 0 , 则PQ在R点拐向左侧后得到QR,等同于点R在PQ线段的左侧
        /// 返回值 等于 0 , 则P,Q,R三点共线。
        /// </remarks>
        public static Int32 DeflectingDirection(PointI P, PointI Q, PointI R)
        {
            //formular
            //折线段的拐向判断方法可以直接由矢量叉积的性质推出。对于有公共端点的线段PQ和QR，通过计算(R - P) * (Q - P)的符号便可以确定折线段的拐向
            //基本算法是(R-P)计算相对于P点的R点坐标,(Q-P)计算相对于P点的Q点坐标。
            //(R-P) * (Q-P)的计算结果是计算以P为相对原点,点R与点Q的顺时针，逆时针方向。
            PointI RP = PointAlgorithm.Substract(R, P);//R-P
            PointI QP = PointAlgorithm.Substract(Q, P);//Q-P
            return PointAlgorithm.Multiple(RP, QP);
        }
        #endregion

        #region Offset
        /// <summary>
        /// 计算偏移折线PL
        /// </summary>
        /// <param name="PL">折线PL</param>
        /// <param name="velocity">偏移速度</param>
        /// <returns>返回偏移折线</returns>
        public static PolylineD Offset(PolylineD PL, PointD velocity)
        {
            List<PointD> list = new List<PointD>();
            for (Int32 i = 0; i < PL.Points.Count; ++i)
            {
                list.Add(PL.Points[i] + velocity);
            }
            return new PolylineD(list.ToArray());
        }
        /// <summary>
        /// 计算偏移折线PL
        /// </summary>
        /// <param name="PL">折线PL</param>
        /// <param name="velocity">偏移速度</param>
        /// <returns>返回偏移折线</returns>
        public static PolylineI Offset(PolylineI PL, PointI velocity)
        {
            List<PointI> list = new List<PointI>();
            for (Int32 i = 0; i < PL.Points.Count; ++i)
            {
                list.Add(PL.Points[i] + velocity);
            }
            return new PolylineI(list.ToArray());
        }
        #endregion

        #region Validate Polyline
        /// <summary>
        /// 获取有效的折线
        /// </summary>
        /// <param name="PL">折线PL</param>
        /// <returns>返回折线</returns>
        public static PolylineD? GetValidatePolyline(PolylineD PL)
        {
            List<PointD> pts = new List<PointD>();
            for(Int32 i = 0;i < PL.Points.Count ;++i)
            {
                if(pts.Contains(PL.Points[i])==false)
                {
                    if(pts.Count > 2)
                    {
                        //斜率相等
                        if(LineAlgorithm.Gradient(new LineD(pts[pts.Count-2],pts[pts.Count-1]))== LineAlgorithm.Gradient(new LineD(pts[pts.Count-1],PL.Points[i])))
                        {
                            pts[pts.Count - 1] = PL.Points[i];
                        }
                        else
                        {
                            pts.Add(PL.Points[i]);
                        }
                    }
                }
            }
            if(pts.Count < 3) return null;
            return new PolylineD(pts.ToArray());
        }        
        /// <summary>
        /// 获取有效的折线
        /// </summary>
        /// <param name="PL">折线PL</param>
        /// <returns>返回折线</returns>
        public static PolylineI? GetValidatePolyline(PolylineI PL)
        {
            return (PolylineI)GetValidatePolyline((PolylineD)PL);
        }

        #endregion
    }
}
