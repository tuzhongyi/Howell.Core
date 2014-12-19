using System;
using System.Collections.Generic;
using System.Text;
using Howell.Drawing.D2;

namespace Howell.Math.Algorithm
{
    /// <summary>
    /// 点计算工具
    /// </summary>
    public static class PointAlgorithm
    {
        #region Equals
        /// <summary>
        /// 判断点P,Q是否重合
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="Q">点Q</param>
        /// <returns>如果重合则返回True，否则返回False。</returns>
        public static Boolean Equals(PointD P, PointD Q)
        {
            if (DoubleAlgorithm.Equals(P.X,Q.X) == false) return false;
            if (DoubleAlgorithm.Equals(P.Y,Q.Y) == false) return false;
            return true;
        }
        /// <summary>
        /// 判断点P,Q是否重合
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="Q">点Q</param>
        /// <returns>如果重合则返回True，否则返回False。</returns>
        public static Boolean Equals(PointI P, PointI Q)
        {
            if (P.X.Equals(Q.X) == false) return false;
            if (P.Y.Equals(Q.Y) == false) return false;
            return true;
        }
        #endregion
        #region MidPoint
        /// <summary>
        /// 计算点P,Q的中点 
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="Q">点Q</param>
        /// <returns>返回点P,Q的中点</returns>
        public static PointD MidPoint(PointD P, PointD Q)
        {
            return new PointD((P.X + Q.X) / 2, (P.Y + Q.Y) / 2);
        }
        /// <summary>
        /// 计算点P,Q的中点 
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="Q">点Q</param>
        /// <returns>返回点P,Q的中点</returns>
        public static PointI MidPoint(PointI P, PointI Q)
        {
            return new PointI((P.X + Q.X) / 2, (P.Y + Q.Y) / 2);
        }
        #endregion
        #region Multiple
        /// <summary>
        /// 计算点 (0,0),P,Q,P+Q组成的平行四边形的带符号的面积
        /// P*Q = -(Q*P)
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="Q">点Q</param>
        /// <returns>返回面积值</returns>
        /// <remarks>
        /// 返回值 大于 0 , 则P在Q的顺时针方向。
        /// 返回值 等于 0 , 则P在Q的逆时针方向。
        /// 返回值 小于 0 , 则P与Q共线，但可能同向也可能反向。
        /// </remarks>
        public static Double Multiple(PointD P, PointD Q)
        {
            //formula
            //P × Q = x1*y2 - x2*y1
            //则矢量叉积定义为由(0,0)、P、Q和P+Q所组成的平行四边形的带符号的面积，即：P * Q = x1*y2 - x2*y1，其结果是一个标量
            //若 P * Q > 0 , 则P在Q的顺时针方向。
            //若 P * Q < 0 , 则P在Q的逆时针方向。
            //若 P * Q = 0 , 则P与Q共线，但可能同向也可能反向。
            Double result = (P.X * Q.Y - Q.X * P.Y);
            if (DoubleAlgorithm.Equals(result,0) == true) return 0;
            return result;
        }
        /// <summary>
        /// 计算点 (0,0),P,Q,P+Q组成的平行四边形的带符号的面积
        /// P*Q = -(Q*P)
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="Q">点Q</param>
        /// <returns>返回面积值</returns>
        /// <remarks>
        /// 返回值 大于 0 , 则P在Q的顺时针方向。
        /// 返回值 小于 0 , 则P在Q的逆时针方向。
        /// 返回值 等于 0 , 则P与Q共线，但可能同向也可能反向。
        /// </remarks>
        public static Int32 Multiple(PointI P, PointI Q)
        {            
            //formula
            //P × Q = x1*y2 - x2*y1
            //则矢量叉积定义为由(0,0)、P、Q和P+Q所组成的平行四边形的带符号的面积，即：P * Q = x1*y2 - x2*y1，其结果是一个标量
            //若 P * Q > 0 , 则P在Q的顺时针方向。
            //若 P * Q < 0 , 则P在Q的逆时针方向。
            //若 P * Q = 0 , 则P与Q共线，但可能同向也可能反向。
            //注意：(P * Q) = -(Q * P)
            return (P.X * Q.Y - Q.X * P.Y);
        }
        /// <summary>
        /// 计算(R-P)和(Q-P)的叉积 
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="Q">点Q</param>
        /// <param name="R">点R</param>
        /// <returns>返回叉积值</returns>
        /// <remarks>
        /// 返回值 大于0 R在矢量PQ的逆时针方向 
        /// 返回值 等于0 R,P,Q 三点共线
        /// 返回值 小于0 R在矢量PQ的顺时针方向
        /// </remarks>
        public static Double Multiple(PointD P, PointD Q, PointD R)
        {
            PointD RP = PointAlgorithm.Substract(R, P);//R-P
            PointD QP = PointAlgorithm.Substract(Q, P);//Q-P
            return PointAlgorithm.Multiple(RP, QP);
        }
        /// <summary>
        /// 计算(R-P)和(Q-P)的叉积 
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="Q">点Q</param>
        /// <param name="R">点R</param>
        /// <returns>返回叉积值</returns>
        /// <remarks>
        /// 返回值 大于0 R在矢量PQ的逆时针方向 
        /// 返回值 等于0 R,P,Q 三点共线
        /// 返回值 小于0 R在矢量PQ的顺时针方向
        /// </remarks>
        public static Int32 Multiple(PointI P, PointI Q, PointI R)
        {
            PointI RP = PointAlgorithm.Substract(R, P);//R-P
            PointI QP = PointAlgorithm.Substract(Q, P);//Q-P
            return PointAlgorithm.Multiple(RP, QP);
        }
      
        #endregion
        #region DotMultiple
        /// <summary>
        /// 得到矢量(Q-P)和(R-P)的点积，如果两个矢量都非零矢量 
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="Q">点Q</param>
        /// <param name="R">点R</param>
        /// <returns>返回点积值。</returns>
        /// <remarks>
        /// 返回值小于0，两矢量夹角为锐角
        /// 返回值等于0，两矢量夹角为直角
        /// 返回值大于0，两矢量夹角为钝角
        /// </remarks>
        public static Double DotMultiple(PointD P, PointD Q, PointD R)
        {
            return ((Q.X - P.X) * (R.X - P.X) + (Q.Y - P.Y) * (R.Y - P.Y));
        }
        /// <summary>
        /// 得到矢量(Q-P)和(R-P)的点积，如果两个矢量都非零矢量 
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="Q">点Q</param>
        /// <param name="R">点R</param>
        /// <returns>返回点积值。</returns>
        /// <remarks>
        /// 返回值小于0，两矢量夹角为锐角
        /// 返回值等于0，两矢量夹角为直角
        /// 返回值大于0，两矢量夹角为钝角
        /// </remarks>
        public static Int32 DotMultiple(PointI P, PointI Q, PointI R)
        {
            return ((Q.X - P.X) * (R.X - P.X) + (Q.Y - P.Y) * (R.Y - P.Y));
        }
        #endregion
        #region Substract
        /// <summary>
        /// 计算点P-Q的值
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="Q">点Q</param>
        /// <returns>返回P-Q的值</returns>
        public static PointD Substract(PointD P, PointD Q)
        {
            //formula
            //x1-x2,y1-y2
            //P - Q = ( x1 - x2 , y1 - y2 )
            return new PointD(P.X - Q.X, P.Y - Q.Y);
        }
        /// <summary>
        /// 计算点P-Q的值
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="Q">点Q</param>
        /// <returns>返回P-Q的值</returns>
        public static PointI Substract(PointI P, PointI Q)
        {
            //formula
            //x1-x2,y1-y2
            //P - Q = ( x1 - x2 , y1 - y2 )
            return new PointI(P.X - Q.X, P.Y - Q.Y);

        }
        #endregion
        #region Distance

        /// <summary>
        /// 计算两点间的距离
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="Q">点Q</param>
        /// <returns>返回距离</returns>
        public static Double Distance(PointD P,PointD Q)
        {
            //formula
            //Sqrt((x1-x2)*(x1-x2) + (y1-y2)*(y1-y2))
            PointD R = Substract(P, Q);
            return System.Math.Sqrt(R.X*R.X + R.Y*R.Y);
        }
        /// <summary>
        /// 计算两点间的距离
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="Q">点Q</param>
        /// <returns>返回距离</returns>
        public static Double Distance(PointI P, PointI Q)
        {
            //formula
            //Sqrt((x1-x2)*(x1-x2) + (y1-y2)*(y1-y2))
            PointI R = Substract(P, Q);
            return System.Math.Sqrt((Double)(R.X * R.X + R.Y * R.Y));
        }
        /// <summary>
        /// 计算点P到线段L所在直线的距离
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="L">线段L</param>
        /// <returns>返回距离</returns>
        public static Double Distance(PointD P, LineD L)
        {
            //formula
            //d=abs(a*x0+b*y0+c)/sqrt(a*a+b*b) 
            //|(x3-x2)*(y1-y2)-(y3-y2)*(x1-x2)|
            //((x3-x2)(x3-x2)+(y3-y2)*(y3-y2))

            //if (p2.x == p3.x)
            //    return abs(p1.y - p2.y);
            //if (p2.y == p3.y)
            //    return abs(p1.x - p2.x);
            //return abs(a * p1.x + b * p1.y + c) / sqrt(a * a + b * b);
            //这里：a=(p3.y-p2.y),b=(p2.x-p3.x),c=p3.x*p2.y-p2.x*p3.x

            //return (Double)((L.End.X - L.Starting.X) * (P.Y - L.Starting.Y) - (L.End.Y - L.Starting.Y) * (P.X - L.Starting.X))
            //    / System.Math.Sqrt((L.End.X - L.Starting.X) * (L.End.X - L.Starting.X) + (L.End.Y - L.Starting.Y) * (L.End.Y - L.Starting.Y));
            return System.Math.Abs(Multiple(L.Starting, L.End, P)) / Distance(L.Starting, L.End);

        }
        /// <summary>
        /// 计算点P到线段L所在直线的距离
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="L">线段L</param>
        /// <returns>返回距离</returns>
        public static Double Distance(PointI P, LineI L)
        {
            //formula
            //d=abs(a*x0+b*y0+c)/sqrt(a*a+b*b) 
            //|(x3-x2)*(y1-y2)-(y3-y2)*(x1-x2)|
            //((x3-x2)(x3-x2)+(y3-y2)*(y3-y2))

            //if (p2.x == p3.x)
            //    return abs(p1.y - p2.y);
            //if (p2.y == p3.y)
            //    return abs(p1.x - p2.x);
            //return abs(a * p1.x + b * p1.y + c) / sqrt(a * a + b * b);
            //这里：a=(p3.y-p2.y),b=(p2.x-p3.x),c=p3.x*p2.y-p2.x*p3.x

            //return (Double)((L.End.X - L.Starting.X) * (P.Y - L.Starting.Y) - (L.End.Y - L.Starting.Y) * (P.X - L.Starting.X))
            //    / System.Math.Sqrt((L.End.X - L.Starting.X) * (L.End.X - L.Starting.X) + (L.End.Y - L.Starting.Y) * (L.End.Y - L.Starting.Y));
            return System.Math.Abs(Multiple(L.Starting, L.End, P)) / Distance(L.Starting, L.End);
        }



        #endregion
        #region Rotate
        /// <summary>
        /// 计算点P以点O为轴心旋转Radian弧度后的点坐标。
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="O">点0</param>
        /// <param name="Radian">弧度</param>
        /// <returns>返回旋转后的点值</returns>
        public static PointD Rotate(PointD P, PointD O, Double Radian)
        {
            PointD result = new PointD();
            result.X = (Double)(P.X - O.X) * System.Math.Cos(Radian) - (Double)(P.Y - O.Y) * System.Math.Sin(Radian) + O.X;
            result.Y = (Double)(P.Y - O.Y) * System.Math.Cos(Radian) + (Double)(P.X - O.X) * System.Math.Sin(Radian) + O.Y;
            return result;
        }
        /// <summary>
        /// 计算点P以点O为轴心旋转Radian弧度后的点坐标。
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="O">点0</param>
        /// <param name="Radian">弧度</param>
        /// <returns>返回旋转后的点值</returns>
        public static PointD Rotate(PointI P, PointI O, Double Radian)
        {
            PointD result = new PointD();
            result.X = (Double)(P.X - O.X) * System.Math.Cos(Radian) - (Double)(P.Y - O.Y) * System.Math.Sin(Radian) + O.X;
            result.Y = (Double)(P.Y - O.Y) * System.Math.Cos(Radian) + (Double)(P.X - O.X) * System.Math.Sin(Radian) + O.Y;
            return result;
        }

        #endregion
        #region Angle
        /// <summary>
        /// 计算矢量Q->P与矢量R->P的夹角弧度
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="Q">点Q</param>
        /// <param name="R">点R</param>
        /// <returns>返回弧度值</returns>
        /// <remarks>
        /// 角度小于PI，返回正值 说明矢量QP在矢量RP的顺时针方向
        /// 角度大于PI，返回负值 说明矢量QP在矢量RP的逆时针方向
        /// </remarks>
        public static Double Angle(PointD P, PointD Q, PointD R)
        {
            /* 返回顶角在o点，起始边为os，终止边为oe的夹角(单位：弧度) 
	            角度小于pi，返回正值 
	            角度大于pi，返回负值 
	            可以用于求线段之间的夹角 
            原理：
	            r = dotmultiply(s,e,o) / (dist(o,s)*dist(o,e))
	            r'= multiply(s,e,o)
	            r >= 1	angle = 0;
	            r <= -1	angle = -PI
	            -1<r<1 && r'>0	angle = arccos(r)
	            -1<r<1 && r'<=0	angle = -arccos(r)
            */ 
            Double result = DotMultiple(P,Q,R)/(Distance(P,Q)*Distance(P,R));
            if (result >= 1.0) return 0;
            if (result <= -1.0) return (-1) * System.Math.PI;             
            //if(result <= 0) return (-1)*System.Math.Acos(result);
            Double fi = System.Math.Acos(result);
            if (fi < System.Math.PI)
            {
                return fi;
            }
            else
            {
                return 2*System.Math.PI - fi;
            }
        }
        /// <summary>
        /// 计算矢量Q->P与矢量R->P的夹角弧度
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="Q">点Q</param>
        /// <param name="R">点R</param>
        /// <returns>返回弧度值</returns>
        /// <remarks>
        /// 角度小于PI，返回正值 说明矢量QP在矢量RP的顺时针方向
        /// 角度大于PI，返回负值 说明矢量QP在矢量RP的逆时针方向
        /// </remarks>
        public static Double Angle(PointI P, PointI Q, PointI R)
        {
            return Angle((PointD)P, (PointD)Q, (PointD)R);
        }
        #endregion
        #region PerpendicularPosition
        /// <summary>
        /// 计算点P在线L的垂足位置
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="L">线L</param>
        /// <returns>返回垂足位置</returns>
        /// <remarks>
        /// 返回值 等于0 垂足=L.End
        /// 返回值 等于1 垂足=L.Starting
        /// 返回值 小于0 垂足在线L的反向延长线上
        /// 返回值 大于1 垂足在线L的正向延长线上
        /// 返回值 (0,1) 垂足在线L上
        /// </remarks>
        public static Double PerpendicularPosition(PointD P, LineD L)
        {
            /* 判断点与线段的关系,用途很广泛 
            本函数是根据下面的公式写的，P是点C到线段AB所在直线的垂足 

                            AC dot AB 
                    r =     --------- 
                             ||AB||^2 
                         (Cx-Ax)(Bx-Ax) + (Cy-Ay)(By-Ay) 
                      = ------------------------------- 
                                      L^2 

                r has the following meaning: 

                    r=0      P = A 
                    r=1      P = B 
                    r<0		 P is on the backward extension of AB 
                    r>1      P is on the forward extension of AB 
                    0<r<1	 P is interior to AB 
            */
            return DotMultiple( L.Starting, L.End, P) / (Distance(L.End, L.Starting) * Distance(L.End, L.Starting)); 
        }
        /// <summary>
        /// 计算点P在线L的垂足位置
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="L">线L</param>
        /// <returns>返回垂足位置</returns>
        /// <remarks>
        /// 返回值 等于0 垂足=L.End
        /// 返回值 等于1 垂足=L.Starting
        /// 返回值 小于0 垂足在线L的反向延长线上
        /// 返回值 大于1 垂足在线L的正向延长线上
        /// 返回值 (0,1) 垂足在线L上
        /// </remarks>
        public static Double PerpendicularPosition(PointI P, LineI L)
        {
            /* 判断点与线段的关系,用途很广泛 
            本函数是根据下面的公式写的，P是点C到线段AB所在直线的垂足 

                            AC dot AB 
                    r =     --------- 
                             ||AB||^2 
                         (Cx-Ax)(Bx-Ax) + (Cy-Ay)(By-Ay) 
                      = ------------------------------- 
                                      L^2 

                r has the following meaning: 

                    r=0      P = A 
                    r=1      P = B 
                    r<0		 P is on the backward extension of AB 
                    r>1      P is on the forward extension of AB 
                    0<r<1	 P is interior to AB 
            */
            return DotMultiple(L.Starting, L.End, P) / (Distance(L.End, L.Starting) * Distance(L.End, L.Starting));
        }
        #endregion
        #region Perpendicular
        /// <summary>
        /// 计算点P到线L垂足
        /// </summary>
        /// <param name="P">线P</param>
        /// <param name="L">点L</param>
        /// <returns>返回垂足点</returns>
        public static PointD Perpendicular(PointD P, LineD L)
        {
            Double r = PerpendicularPosition(P,L);
            PointD result = new PointD();
            result.X = L.Starting.X + r * (L.End.X - L.Starting.X);
            result.Y = L.Starting.Y + r * (L.End.Y - L.Starting.Y);
            return result; 
        }
        /// <summary>
        /// 计算点P到线L垂足
        /// </summary>
        /// <param name="P">线P</param>
        /// <param name="L">点L</param>
        /// <returns>返回垂足点</returns>
        public static PointD Perpendicular(PointI P, LineI L)
        {
            Double r = PerpendicularPosition(P, L);
            PointD result = new PointD();
            result.X = L.Starting.X + r * (L.End.X - L.Starting.X);
            result.Y = L.Starting.Y + r * (L.End.Y - L.Starting.Y);
            return result;
        }
        #endregion
        #region Closest Point
        /// <summary>
        /// 计算点P到线L的最近点,该点在线上。不一定是垂足,应为垂足不一定在线上。 
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="L">线L</param>
        /// <returns>返回最近点</returns>
        public static PointD ClosestPoint(PointD P, LineD L)
        {
            Double r = PerpendicularPosition(P, L);            
            if (r < 0)//最近点是起点
            {
                return L.Starting;
            }
            if (r > 1)//最近点是终点
            {
                return L.End;
            }
            return Perpendicular(P, L);
        }
        /// <summary>
        /// 计算点P到线L的最近点,该点在线上。不一定是垂足,应为垂足不一定在线上。 
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="L">线L</param>
        /// <returns>返回最近点</returns>
        public static PointD ClosestPoint(PointI P, LineI L)
        {
            Double r = PerpendicularPosition(P, L);
            if (r < 0)//最近点是起点
            {
                return L.Starting;
            }
            if (r > 1)//最近点是终点
            {
                return L.End;
            }
            return Perpendicular(P, L);
        }
        #endregion
        #region Closest Distance

        /// <summary>
        /// 计算点P到线L的最近距离
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="L">线L</param>
        /// <returns>返回最近距离</returns>
        public static Double ClosestDistance(PointD P, LineD L)
        {
            return Distance(ClosestPoint(P, L), (PointD)P);
        }
        /// <summary>
        /// 计算点P到线L的最近距离
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="L">线L</param>
        /// <returns>返回最近距离</returns>
        public static Double ClosestDistance(PointI P, LineI L)
        {
            return Distance(ClosestPoint(P, L), (PointD)P);
        }
        /// <summary>
        /// 计算点P到折线PL的最近距离
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="PL">折线PL</param>
        /// <returns>返回最近距离</returns>
        public static Double ClosestDistance(PointD P, PolylineD PL)
        {
            Double result = Double.MaxValue;
            foreach (LineD S in PL)
            {
                result = System.Math.Min(result, ClosestDistance(P, S));
            }
            return result;
        }
        /// <summary>
        /// 计算点P到折线PL的最近距离
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="PL">折线PL</param>
        /// <returns>返回最近距离</returns>
        public static Double ClosestDistance(PointI P, PolylineI PL)
        {
            Double result = Double.MaxValue;
            foreach (LineI S in PL)
            {
                result = System.Math.Min(result, ClosestDistance(P, S));
            }
            return result;
        }

        /// <summary>
        /// 计算点P到多边形PG的最近距离
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="PG">多边形PG</param>
        /// <returns>返回最近距离</returns>
        public static Double ClosestDistance(PointD P, PolygonD PG)
        {
            Double result = Double.MaxValue;
            foreach (LineD S in PG)
            {
                result = System.Math.Min(result, ClosestDistance(P, S));
            }
            return result;
        }
        /// <summary>
        /// 计算点P到多边形PG的最近距离
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="PG">多边形PG</param>
        /// <returns>返回最近距离</returns>
        public static Double ClosestDistance(PointI P, PolygonI PG)
        {
            Double result = Double.MaxValue;
            foreach (LineI S in PG)
            {
                result = System.Math.Min(result, ClosestDistance(P, S));
            }
            return result;
        }
        #endregion
        #region Offset
        /// <summary>
        /// 偏移点计算
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="velocity">偏移速度</param>
        /// <returns>返回偏移点。</returns>
        public static PointD Offset(PointD P, PointD velocity)
        {
            return P + velocity;
        }
        /// <summary>
        /// 偏移点计算
        /// </summary>
        /// <param name="P">点P</param>
        /// <param name="velocity">偏移速度</param>
        /// <returns>返回偏移点。</returns>
        public static PointI Offset(PointI P, PointI velocity)
        {
            return P + velocity;
        }
        #endregion
    }
}
