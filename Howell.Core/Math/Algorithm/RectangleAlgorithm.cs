using System;
using System.Collections.Generic;
using System.Text;
using Howell.Drawing.D2;

namespace Howell.Math.Algorithm
{
    /// <summary>
    /// 矩形计算
    /// </summary>
    public static class RectangleAlgorithm
    {
        #region InRectangle
        /// <summary>
        /// 点是否在区域内
        /// </summary>
        /// <param name="Rect">矩形区域</param>
        /// <param name="P">点P</param>
        /// <returns>
        /// 返回True表示点P在区域内，返回False则不在区域内.
        /// </returns>
        public static Boolean InRectangle(RectangleD Rect, PointD P)
        {
            MinMax<Double> MX = new MinMax<Double>(Rect.Left, Rect.Right);
            MinMax<Double> MY = new MinMax<Double>(Rect.Top, Rect.Bottom);
            return (MX.InRange(P.X) && MY.InRange(P.Y));
        }
        /// <summary>
        /// 点是否在区域内
        /// </summary>
        /// <param name="Rect">矩形区域</param>
        /// <param name="P">点P</param>
        /// <returns>
        /// 返回True表示点P在区域内，返回False则不在区域内.
        /// </returns>
        public static Boolean InRectangle(RectangleI Rect, PointI P)
        {
            MinMax<Int32> MX = new MinMax<Int32>(Rect.Left, Rect.Right);
            MinMax<Int32> MY = new MinMax<Int32>(Rect.Top, Rect.Bottom);
            return (MX.InRange(P.X) && MY.InRange(P.Y));
        }
        /// <summary>
        /// 线是否在区域内
        /// </summary>
        /// <param name="Rect">矩形区域</param>
        /// <param name="L">线L</param>
        /// <returns>
        /// 返回True表示线L在区域内，返回False则不在区域内.
        /// </returns>
        public static Boolean InRectangle(RectangleD Rect, LineD L)
        {
            return (InRectangle(Rect, L.Starting) && InRectangle(Rect, L.End));
        }
        /// <summary>
        /// 线是否在区域内
        /// </summary>
        /// <param name="Rect">矩形区域</param>
        /// <param name="L">线L</param>
        /// <returns>
        /// 返回True表示线L在区域内，返回False则不在区域内.
        /// </returns>
        public static Boolean InRectangle(RectangleI Rect, LineI L)
        {
            return (InRectangle(Rect, L.Starting) && InRectangle(Rect, L.End));
        }
        /// <summary>
        /// 折线是否在区域内
        /// </summary>
        /// <param name="Rect">矩形区域</param>
        /// <param name="PL">折线PL</param>
        /// <returns>
        /// 返回True表示折线PL在区域内，返回False则不在区域内.
        /// </returns>
        public static Boolean InRectangle(RectangleD Rect, PolylineD PL)
        {
            if(PL.Points == null) return false;
            for (Int32 i = 0; i < PL.Points.Count; ++i)
            {
                if (false == InRectangle(Rect, PL.Points[i])) return false;
            }
            return true;
        }
        /// <summary>
        /// 折线是否在区域内
        /// </summary>
        /// <param name="Rect">矩形区域</param>
        /// <param name="PL">折线PL</param>
        /// <returns>
        /// 返回True表示折线PL在区域内，返回False则不在区域内.
        /// </returns>
        public static Boolean InRectangle(RectangleI Rect, PolylineI PL)
        {
            if (PL.Points == null) return false;
            for (Int32 i = 0; i < PL.Points.Count; ++i)
            {
                if (false == InRectangle(Rect, PL.Points[i])) return false;
            }
            return true;
        }
        /// <summary>
        /// 矩形R是否在区域内
        /// </summary>
        /// <param name="Rect">矩形区域</param>
        /// <param name="R">矩形R</param>
        /// <returns>
        /// 返回True表示矩形R在区域内，返回False则不在区域内.
        /// </returns>
        public static Boolean InRectangle(RectangleD Rect, RectangleD R)
        {
            //只要比较左右边界和上下边界就可以了。
            if (false == InRectangle(Rect,new PointD(R.Left,R.Top))) return false;
            if (false == InRectangle(Rect, new PointD(R.Right, R.Bottom))) return false;
            return true;
        }
        /// <summary>
        /// 矩形R是否在区域内
        /// </summary>
        /// <param name="Rect">矩形区域</param>
        /// <param name="R">矩形R</param>
        /// <returns>
        /// 返回True表示矩形R在区域内，返回False则不在区域内.
        /// </returns>
        public static Boolean InRectangle(RectangleI Rect, RectangleI R)
        {
            //只要比较左右边界和上下边界就可以了。
            if (false == InRectangle(Rect, new PointD(R.Left, R.Top))) return false;
            if (false == InRectangle(Rect, new PointD(R.Right, R.Bottom))) return false;
            return true;
        }
        /// <summary>
        /// 多边形是否在区域内
        /// </summary>
        /// <param name="Rect">矩形区域</param>
        /// <param name="PL">多边形PL</param>
        /// <returns>
        /// 返回True表示多边形PL在区域内，返回False则不在区域内.
        /// </returns>
        public static Boolean InRectangle(RectangleD Rect, PolygonD PL)
        {
            if (PL.Vertex == null) return false;
            for (Int32 i = 0; i < PL.Vertex.Count; ++i)
            {
                if (false == InRectangle(Rect, PL.Vertex[i])) return false;
            }
            return true;
        }
        /// <summary>
        /// 多边形是否在区域内
        /// </summary>
        /// <param name="Rect">矩形区域</param>
        /// <param name="PL">多边形PL</param>
        /// <returns>
        /// 返回True表示多边形PL在区域内，返回False则不在区域内.
        /// </returns>
        public static Boolean InRectangle(RectangleI Rect, PolygonI PL)
        {
            if (PL.Vertex == null) return false;
            for (Int32 i = 0; i < PL.Vertex.Count; ++i)
            {
                if (false == InRectangle(Rect, PL.Vertex[i])) return false;
            }
            return true;
        }
        /// <summary>
        /// 圆形是否在矩形内
        /// </summary>
        /// <param name="R">矩形R</param>
        /// <param name="C">圆形C</param>
        /// <returns> 返回True表示圆形C在区域内，返回False则不在区域内.</returns>
        public static Boolean InRectangle(RectangleD R, CircleD C)
        {
            //很容易证明，圆在矩形中的充要条件是：圆心在矩形中且圆的半径小于等于圆心到矩形四边的距离的最小值。
            if (InRectangle(R, C.Center) == false) return false;
            Double MinXDistance = System.Math.Min((C.X - R.Left), (R.Right - C.X));
            Double MinYDistance = System.Math.Min((C.Y - R.Top), (R.Bottom - C.Y));
            if (C.Radius <= MinXDistance && C.Radius <= MinYDistance) return true;
            return false;
        }

        /// <summary>
        /// 圆形是否在矩形内
        /// </summary>
        /// <param name="R">矩形R</param>
        /// <param name="C">圆形C</param>
        /// <returns> 返回True表示圆形C在区域内，返回False则不在区域内.</returns>
        public static Boolean InRectangle(RectangleI R, CircleI C)
        {
            //很容易证明，圆在矩形中的充要条件是：圆心在矩形中且圆的半径小于等于圆心到矩形四边的距离的最小值。
            if (InRectangle(R, C.Center) == false) return false;
            Int32 MinXDistance = System.Math.Min((C.X - R.Left), (R.Right - C.X));
            Int32 MinYDistance = System.Math.Min((C.Y - R.Top), (R.Bottom - C.Y));
            if (C.Radius <= MinXDistance && C.Radius <= MinYDistance) return true;
            return false;
        }

        #endregion
        #region HasIntersection
        /// <summary>
        /// 判断R1与R2是否有交集
        /// </summary>
        /// <param name="R1">矩形R1</param>
        /// <param name="R2">矩形R2</param>
        /// <returns>如果有交集返回True,否则返回False。</returns>
        public static Boolean HasIntersection(RectangleD R1, RectangleD R2)
        {
            //第一个矩形左下角x1,y1，右上角x2,y2，第二个左下x3,y3,右上x4,y4
            //1 当x2<x3 or x4<x1 or y4<y1 or y3>y2 没有交集
            if (R1.Right < R2.Left) return false; //x2<x3
            if (R2.Right < R1.Left) return false; //x4<x1
            if (R2.Bottom < R1.Top) return false; //y4<y1
            if (R1.Bottom < R2.Top) return false; //y2<y3
            return true;
        }
        /// <summary>
        /// 判断R1与R2是否有交集
        /// </summary>
        /// <param name="R1">矩形R1</param>
        /// <param name="R2">矩形R2</param>
        /// <returns>如果有交集返回True,否则返回False。</returns>
        public static Boolean HasIntersection(RectangleI R1, RectangleI R2)
        {
            //第一个矩形左下角x1,y1，右上角x2,y2，第二个左下x3,y3,右上x4,y4
            //1 当x2<x3 or x4<x1 or y4<y1 or y3>y2 没有交集
            if (R1.Right < R2.Left) return false; //x2<x3
            if (R2.Right < R1.Left) return false; //x4<x1
            if (R2.Bottom < R1.Top) return false; //y4<y1
            if (R1.Bottom < R2.Top) return false; //y2<y3
            return true;
        }
        #endregion
        #region Intersection
        /// <summary>
        /// 计算矩形交集的面积值
        /// </summary>
        /// <param name="R1">Rectangle 1</param>
        /// <param name="R2">Rectangle 2</param>
        /// <returns>
        /// 返回交集的矩形。
        /// </returns>
        public static RectangleD? Intersection(RectangleD R1, RectangleD R2)
        {
            if (HasIntersection(R1, R2) == false) return null;
            //2 除去1的情况后交集为 (min(y4,y2)-max(y1,y3))*(min(x4,x2)-max(x1,x3))          
            RectangleD result = new RectangleD();
            result.X = System.Math.Max(R1.Left, R2.Left);
            result.Y = System.Math.Max(R1.Top, R2.Top);
            result.Width = System.Math.Min(R1.Right, R2.Right) - result.X;
            result.Height = System.Math.Min(R1.Bottom, R2.Bottom) - result.Y;
            return result;
        }
        /// <summary>
        /// 计算矩形交集的面积值
        /// </summary>
        /// <param name="R1">Rectangle 1</param>
        /// <param name="R2">Rectangle 2</param>
        /// <returns>
        /// 返回交集的矩形。
        /// </returns>
        public static RectangleI? Intersection(RectangleI R1, RectangleI R2)
        {
            if (HasIntersection(R1, R2) == false) return null;
            //2 除去1的情况后交集为 (min(y4,y2)-max(y1,y3))*(min(x4,x2)-max(x1,x3))            
            RectangleI result = new RectangleI();
            result.X = System.Math.Max(R1.Left, R2.Left);
            result.Y = System.Math.Max(R1.Top, R2.Top);
            result.Width = System.Math.Min(R1.Right, R2.Right) - result.X;
            result.Height = System.Math.Min(R1.Bottom, R2.Bottom) - result.Y;
            return result;
        }
        #endregion
        #region Area
        /// <summary>
        /// 计算矩形面积
        /// </summary>
        /// <param name="R">矩形R</param>
        /// <returns>返回面积。</returns>
        public static Double Area(RectangleI R)
        {
            return R.Width * R.Height;
        }
        /// <summary>
        /// 计算矩形面积
        /// </summary>
        /// <param name="R">矩形R</param>
        /// <returns>返回面积。</returns>
        public static Double Area(RectangleD R)
        {
            return R.Width * R.Height;
        }
        #endregion
        #region Offset
        /// <summary>
        /// 计算偏移后的矩形
        /// </summary>
        /// <param name="R">矩形R</param>
        /// <param name="velocity">偏移速度</param>
        /// <returns>返回偏移后的矩形</returns>
        public static RectangleD Offset(RectangleD R, PointD velocity)
        {
            return new RectangleD(R.Location + velocity, R.Size);
        }
        /// <summary>
        /// 计算偏移后的矩形
        /// </summary>
        /// <param name="R">矩形R</param>
        /// <param name="velocity">偏移速度</param>
        /// <returns>返回偏移后的矩形</returns>
        public static RectangleI Offset(RectangleI R, PointI velocity)
        {
            return new RectangleI(R.Location + velocity, R.Size);
        }
        #endregion
    }
}
