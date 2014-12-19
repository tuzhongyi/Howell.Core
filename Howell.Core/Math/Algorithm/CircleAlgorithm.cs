using System;
using System.Collections.Generic;
using System.Text;
using Howell.Drawing.D2;

namespace Howell.Math.Algorithm
{
    /// <summary>
    /// 圆计算方法
    /// </summary>
    public static class CircleAlgorithm
    {
        /// <summary>
        /// PI值
        /// </summary>
        public const Double PI = System.Math.PI;

        #region OnCircle

        /// <summary>
        /// 判断点P是否在圆边界上
        /// </summary>
        /// <param name="C">圆C</param>
        /// <param name="P">点P</param>
        /// <returns>如果在圆边界上返回True，否则返回False。</returns>
        public static Boolean OnCircle(CircleD C, PointD P)
        {
            return DoubleAlgorithm.Equals(Distance(C, P),0);
        }
        /// <summary>
        /// 判断点P是否在圆边界上
        /// </summary>
        /// <param name="C">圆C</param>
        /// <param name="P">点P</param>
        /// <returns>如果在圆边界上返回True，否则返回False。</returns>
        public static Boolean OnCircle(CircleI C, PointI P)
        {
            return DoubleAlgorithm.Equals(Distance(C, P), 0);
        }
        #endregion

        #region InCircle

        /// <summary>
        /// 判断点P是否在圆内
        /// </summary>
        /// <param name="C">圆C</param>
        /// <param name="P">点P</param>
        /// <returns>如果在圆内返回True，否则返回False。</returns>
        public static Boolean InCircle(CircleD C, PointD P)
        {
            //判断点是否在圆内：
　　        //计算圆心到该点的距离，如果小于等于半径则该点在圆内。 
            Double D = PointAlgorithm.Distance(P, C.Center);
            return ((D < C.Radius) || DoubleAlgorithm.Equals(D,C.Radius));
        }
        /// <summary>
        /// 判断点P是否在圆内
        /// </summary>
        /// <param name="C">圆C</param>
        /// <param name="P">点P</param>
        /// <returns>如果在圆内返回True，否则返回False。</returns>
        public static Boolean InCircle(CircleI C, PointI P)
        {
            //判断点是否在圆内：
　　        //计算圆心到该点的距离，如果小于等于半径则该点在圆内。 
            Double D = PointAlgorithm.Distance(P, C.Center);
            return ((D < C.Radius) || DoubleAlgorithm.Equals(D, C.Radius));
        }
        /// <summary>
        /// 判断线段L是否在圆内
        /// </summary>
        /// <param name="C">圆C</param>
        /// <param name="L">线段L</param>
        /// <returns>如果在圆内返回True，否则返回False。</returns>
        public static Boolean InCircle(CircleD C, LineD L)
        {
            //判断点是否在圆内：
　　        //计算圆心到该点的距离，如果小于等于半径则该点在圆内。 
            if( PointAlgorithm.Distance(L.Starting, C.Center) > C.Radius ) return false;
            if(PointAlgorithm.Distance(L.End,C.Center) > C.Radius) return false;
            return true;
        }
        /// <summary>
        /// 判断线段L是否在圆内
        /// </summary>
        /// <param name="C">圆C</param>
        /// <param name="L">线段L</param>
        /// <returns>如果在圆内返回True，否则返回False。</returns>
        public static Boolean InCircle(CircleI C, LineI L)
        {
            //判断点是否在圆内：
　　        //计算圆心到该点的距离，如果小于等于半径则该点在圆内。 
            if( PointAlgorithm.Distance(L.Starting, C.Center) > C.Radius ) return false;
            if(PointAlgorithm.Distance(L.End,C.Center) > C.Radius) return false;
            return true;
        }
        /// <summary>
        /// 判断多边形PG是否在圆内
        /// </summary>
        /// <param name="C">圆C</param>
        /// <param name="PG">多边形PG</param>
        /// <returns>如果在圆内返回True，否则返回False。</returns>
        public static Boolean InCircle(CircleD C, PolygonD PG)
        {
            if(PG.Vertex == null) return false;
            for(Int32 i = 0;i <PG.Vertex.Count;++i)
            {
                if(PointAlgorithm.Distance(PG.Vertex[i],C.Center) > C.Radius) return false;
            }
            return true;
        }        
        /// <summary>
        /// 判断多边形PG是否在圆内
        /// </summary>
        /// <param name="C">圆C</param>
        /// <param name="PG">多边形PG</param>
        /// <returns>如果在圆内返回True，否则返回False。</returns>
        public static Boolean InCircle(CircleI C, PolygonI PG)
        {
            if(PG.Vertex == null) return false;
            for(Int32 i = 0;i <PG.Vertex.Count;++i)
            {
                if(PointAlgorithm.Distance(PG.Vertex[i],C.Center) > C.Radius) return false;
            }
            return true;
        }
        /// <summary>
        /// 判断折线PL是否在圆内
        /// </summary>
        /// <param name="C">圆C</param>
        /// <param name="PL">折线PL</param>
        /// <returns>如果在圆内返回True，否则返回False。</returns>
        public static Boolean InCircle(CircleD C, PolylineD PL)
        {
            if (PL.Points == null) return false;
            for (Int32 i = 0; i < PL.Points.Count; ++i)
            {
                if (PointAlgorithm.Distance(PL.Points[i], C.Center) > C.Radius) return false;
            }
            return true;
        }
        /// <summary>
        /// 判断折线PL是否在圆内
        /// </summary>
        /// <param name="C">圆C</param>
        /// <param name="PL">折线PL</param>
        /// <returns>如果在圆内返回True，否则返回False。</returns>
        public static Boolean InCircle(CircleI C, PolylineI PL)
        {
            if (PL.Points == null) return false;
            for (Int32 i = 0; i < PL.Points.Count; ++i)
            {
                if (PointAlgorithm.Distance(PL.Points[i], C.Center) > C.Radius) return false;
            }
            return true;
        }
        /// <summary>
        /// 判断矩形R是否在圆内
        /// </summary>
        /// <param name="C">圆C</param>
        /// <param name="R">矩形R</param>
        /// <returns>如果在圆内返回True，否则返回False。</returns>
        public static Boolean InCircle(CircleD C, RectangleD R)
        {
            if (PointAlgorithm.Distance(new PointD(R.Left, R.Top), C.Center) > C.Radius) return false;
            if (PointAlgorithm.Distance(new PointD(R.Right, R.Bottom), C.Center) > C.Radius) return false;
            return true;
        }
        /// <summary>
        /// 判断折线PL是否在圆内
        /// </summary>
        /// <param name="C">圆C</param>
        /// <param name="R">矩形R</param>
        /// <returns>如果在圆内返回True，否则返回False。</returns>
        public static Boolean InCircle(CircleI C, RectangleI R)
        {
            if (PointAlgorithm.Distance(new PointI(R.Left, R.Top), C.Center) > C.Radius) return false;
            if (PointAlgorithm.Distance(new PointI(R.Right, R.Bottom), C.Center) > C.Radius) return false;
            return true;
        }

        /// <summary>
        /// 判断圆C2是否在圆C1内
        /// </summary>
        /// <param name="C1">圆C1</param>
        /// <param name="C2">圆C2</param>
        /// <returns>如果在圆内返回True，否则返回False。</returns>
        public static Boolean InCircle(CircleD C1, CircleD C2)
        {
            //formula
            //C2的中心点到C1中心点的距离 加上C2的半径小于C1的半径
            return ((PointAlgorithm.Distance(C1.Center, C2.Center) + C2.Radius) > C1.Radius) ? false : true;
        }
        /// <summary>
        /// 判断圆C2是否在圆C1内
        /// </summary>
        /// <param name="C1">圆C1</param>
        /// <param name="C2">圆C2</param>
        /// <returns>如果在圆内返回True，否则返回False。</returns>
        public static Boolean InCircle(CircleI C1, CircleI C2)
        {
            //formula
            //C2的中心点到C1中心点的距离 加上C2的半径小于C1的半径
            return ((PointAlgorithm.Distance(C1.Center, C2.Center) + C2.Radius) > C1.Radius) ? false : true;
        }

        
        #endregion

        #region Distance
        /// <summary>
        /// 计算点到圆的距离
        /// </summary>
        /// <param name="C">圆C</param>
        /// <param name="P">点P</param>
        /// <returns>返回点到圆周的距离。</returns>
        /// <remarks>
        /// 返回值小于0 表示点在圆内。
        /// 返回值等于0 表示点在圆周上。
        /// 返回值大于0 表示点在圆外。
        /// </remarks>
        public static Double Distance(CircleD C, PointD P)
        {
            return PointAlgorithm.Distance(C.Center, P) - C.Radius;
        }
        /// <summary>
        /// 计算点到圆的距离
        /// </summary>
        /// <param name="C">圆C</param>
        /// <param name="P">点P</param>
        /// <returns>返回点到圆周的距离。</returns>
        /// <remarks>
        /// 返回值小于0 表示点在圆内。
        /// 返回值等于0 表示点在圆周上。
        /// 返回值大于0 表示点在圆外。
        /// </remarks>
        public static Double Distance(CircleI C, PointI P)
        {
            return PointAlgorithm.Distance(C.Center, P) - C.Radius;
        }
        /// <summary>
        /// 计算线L到圆C的距离
        /// </summary>
        /// <param name="C">圆C</param>
        /// <param name="L">线L</param>
        /// <returns>返回线到圆周的距离。</returns>
        /// <remarks>
        /// 返回值小于0 表示线在圆内或与圆周相交。
        /// 返回值等于0 表示线在圆周上与圆周相切。
        /// 返回值大于0 表示线在圆外与圆周没有交点。
        /// </remarks>
        public static Double Distance(CircleD C, LineD L)
        {
            return PointAlgorithm.Distance(C.Center, L) - C.Radius;
        }
        /// <summary>
        /// 计算线L到圆C的距离
        /// </summary>
        /// <param name="C">圆C</param>
        /// <param name="L">线L</param>
        /// <returns>返回线到圆周的距离。</returns>
        /// <remarks>
        /// 返回值小于0 表示线在圆内或与圆周相交。
        /// 返回值等于0 表示线在圆周上与圆周相切。
        /// 返回值大于0 表示线在圆外与圆周没有交点。
        /// </remarks>
        public static Double Distance(CircleI C, LineI L)
        {
            return PointAlgorithm.Distance(C.Center, L) - C.Radius;
        }


        #endregion

        #region Create Circle

        /// <summary>
        /// 根据点P,Q,R三点确定一个圆,注意三点不能共线
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="Q">点Q</param>
        /// <param name="R">点R</param>
        /// <returns>返回圆，如果圆不存在则返回null.</returns>
        public static CircleD? CreateCircle(PointD P, PointD Q, PointD R)
        {
            if (DoubleAlgorithm.Equals(LineAlgorithm.Position(P, Q, R),0)) return null;//三点共线无法确定圆
            //formula 
            //(x-a)^2+(y-b)^2=r^2
            //f1:(x1-a)^2 + (y1-b)^2 = r^2
            //f2:(x2-a)^2 + (y2-b)^2 = r^2
            //f3:(x3-a)^2 + (y3-b)^2 = r^2
            //f1=f2: x1^2-2ax1+y1^2-2by1= x2^2-2ax2+y2^2-2by2;
            //a=(((X(1)^2-X(2)^2+Y(1)^2-Y(2)^2)*(Y(2)-Y(3)))-((X(2)^2-X(3)^2+Y(2)^2-Y(3)^2)*(Y(1)-Y(2))))/(2*(X(1)-X(2))*(Y(2)-Y(3))-2*(X(2)-X(3))*(Y(1)-Y(2)))
            //b=(((X(1)^2-X(2)^2+Y(1)^2-Y(2)^2)*(X(2)-X(3)))-((X(2)^2-X(3)^2+Y(2)^2-Y(3)^2)*(X(1)-X(2))))/(2*(Y(1)-Y(2))*(X(2)-X(3))-2*(Y(2)-Y(3))*(X(1)-X(2)))
            Double a = (((P.X * P.X - Q.X * Q.X + P.Y * P.Y - Q.Y * Q.Y) * (Q.Y - R.Y)) - ((Q.X * Q.X - R.X * R.X + Q.Y * Q.Y - R.Y * R.Y) * (P.Y - Q.Y))) / (2 * (P.X - Q.X) * (Q.Y - R.Y) - 2 * (Q.X - R.X) * (P.Y - Q.Y));
            Double b = (((P.X * P.X - Q.X * Q.X + P.Y * P.Y - Q.Y * Q.Y) * (Q.X - R.X)) - ((Q.X * Q.X - R.X * R.X + Q.Y * Q.Y - R.Y * R.Y) * (P.X - Q.X))) / (2 * (P.Y - Q.Y) * (Q.X - R.X) - 2 * (Q.Y - R.Y) * (P.X - Q.X));
            Double r = PointAlgorithm.Distance(P, new PointD(a, b));
            return new CircleD(a, b, r);
        }

        /// <summary>
        /// 根据点P,Q,R三点确定一个圆,注意三点不能共线
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="Q">点Q</param>
        /// <param name="R">点R</param>
        /// <returns>返回圆，如果圆不存在则返回null.</returns>
        public static CircleD? CreateCircle(PointI P, PointI Q, PointI R)
        {
            if (DoubleAlgorithm.Equals(LineAlgorithm.Position(P, Q, R),0)) return null;//三点共线无法确定圆
            //formula 
            //(x-a)^2+(y-b)^2=r^2
            //f1:(x1-a)^2 + (y1-b)^2 = r^2
            //f2:(x2-a)^2 + (y2-b)^2 = r^2
            //f3:(x3-a)^2 + (y3-b)^2 = r^2
            //f1=f2: x1^2-2ax1+y1^2-2by1= x2^2-2ax2+y2^2-2by2;
            //a=(((X(1)^2-X(2)^2+Y(1)^2-Y(2)^2)*(Y(2)-Y(3)))-((X(2)^2-X(3)^2+Y(2)^2-Y(3)^2)*(Y(1)-Y(2))))/(2*(X(1)-X(2))*(Y(2)-Y(3))-2*(X(2)-X(3))*(Y(1)-Y(2)))
            //b=(((X(1)^2-X(2)^2+Y(1)^2-Y(2)^2)*(X(2)-X(3)))-((X(2)^2-X(3)^2+Y(2)^2-Y(3)^2)*(X(1)-X(2))))/(2*(Y(1)-Y(2))*(X(2)-X(3))-2*(Y(2)-Y(3))*(X(1)-X(2)))
            Double a = (Double)(((P.X * P.X - Q.X * Q.X + P.Y * P.Y - Q.Y * Q.Y) * (Q.Y - R.Y)) - ((Q.X * Q.X - R.X * R.X + Q.Y * Q.Y - R.Y * R.Y) * (P.Y - Q.Y))) / (Double)(2 * (P.X - Q.X) * (Q.Y - R.Y) - 2 * (Q.X - R.X) * (P.Y - Q.Y));
            Double b = (Double)(((P.X * P.X - Q.X * Q.X + P.Y * P.Y - Q.Y * Q.Y) * (Q.X - R.X)) - ((Q.X * Q.X - R.X * R.X + Q.Y * Q.Y - R.Y * R.Y) * (P.X - Q.X))) / (Double)(2 * (P.Y - Q.Y) * (Q.X - R.X) - 2 * (Q.Y - R.Y) * (P.X - Q.X));
            Double r = PointAlgorithm.Distance(P, new PointD(a, b));
            return new CircleD(a, b, r);
        }
        #endregion

        #region Area
        /// <summary>
        /// 计算圆形面积
        /// </summary>
        /// <param name="C">圆形C</param>
        /// <returns>返回面积。</returns>
        public static Double Area(CircleI C)
        {
            //formula PI*R*R
            return System.Math.PI * C.Radius * C.Radius;
        }

        /// <summary>
        /// 计算圆形面积
        /// </summary>
        /// <param name="C">圆形C</param>
        /// <returns>返回面积。</returns>
        public static Double Area(CircleD C)
        {
            //formula PI*R*R
            return System.Math.PI * C.Radius * C.Radius;
        }


        #endregion

        #region Offset
        /// <summary>
        /// 计算圆形的偏移
        /// </summary>
        /// <param name="C">圆形C</param>
        /// <param name="velocity">偏移速度。</param>
        /// <returns>返回偏移后的圆形。</returns>
        public static CircleD Offset(CircleD C, PointD velocity)
        {
            return new CircleD(C.Center + velocity, C.Radius);
        }
        /// <summary>
        /// 计算圆形的偏移
        /// </summary>
        /// <param name="C">圆形C</param>
        /// <param name="velocity">偏移速度。</param>
        /// <returns>返回偏移后的圆形。</returns>
        public static CircleI Offset(CircleI C, PointI velocity)
        {
            return new CircleI(C.Center + velocity, C.Radius);
        }

        #endregion

    }
}
