using System;
using System.Collections.Generic;
using System.Text;
using Howell.Drawing.D2;

namespace Howell.Math.Algorithm
{
    /// <summary>
    /// 线段计算
    /// </summary>
    public static class LineAlgorithm
    {

        #region MidPoint
        /// <summary>
        /// 计算线段L的中点 
        /// </summary>
        /// <param name="L">线段L</param>
        /// <returns>返回线段L的中点</returns>
        public static PointD MidPoint(LineD L)
        {
            return new PointD((L.Starting.X + L.End.X) / 2, (L.Starting.Y + L.End.Y) / 2);
        }
        /// <summary>
        /// 计算线段L的中点 
        /// </summary>
        /// <param name="L">线段L</param>
        /// <returns>返回线段L的中点</returns>
        public static PointI MidPoint(LineI L)
        {
            return new PointI((L.Starting.X + L.End.X) / 2, (L.Starting.Y + L.End.Y) / 2);
        }
        #endregion
        #region Position
        /// <summary>
        /// 判断点与线的位置
        /// </summary>
        /// <param name="L">L线</param>
        /// <param name="R">R点</param>
        /// <returns>返回偏转方向</returns>
        /// <remarks>
        /// 假设L线的是 P->Q PQ为线的2个顶点
        /// 返回值 大于 0 , 则PQ在R点拐向右侧后得到QR,等同于点R在PQ线段的右侧
        /// 返回值 小于 0 , 则PQ在R点拐向左侧后得到QR,等同于点R在PQ线段的左侧
        /// 返回值 等于 0 , 则P,Q,R三点共线。
        /// </remarks>
        public static Double Position(LineD L, PointD R)
        {
            //formular
            //折线段的拐向判断方法可以直接由矢量叉积的性质推出。对于有公共端点的线段PQ和QR，通过计算(R - P) * (Q - P)的符号便可以确定折线段的拐向
            //基本算法是(R-P)计算相对于P点的R点坐标,(Q-P)计算相对于P点的Q点坐标。
            //(R-P) * (Q-P)的计算结果是计算以P为相对原点,点R与点Q的顺时针，逆时针方向。
            //PointD P = L.Starting;
            //PointD Q = L.End;
            //PointD RP = PointAlgorithm.Substract(R, P);//R-P
            //PointD QP = PointAlgorithm.Substract(Q, P);//Q-P
            //return PointAlgorithm.Multiple(RP, QP);
            return PointAlgorithm.Multiple(L.Starting, L.End, R);
        }
        /// <summary>
        /// 判断折线的偏转方向/线段拐向
        /// </summary>
        /// <param name="L">L线段</param>
        /// <param name="R">R点</param>
        /// <returns>返回偏转方向</returns>
        /// <remarks>
        /// 假设L线的是 P->Q PQ为线的2个顶点
        /// 返回值 大于 0 , 则PQ在R点拐向右侧后得到QR,等同于点R在PQ线段的右侧
        /// 返回值 小于 0 , 则PQ在R点拐向左侧后得到QR,等同于点R在PQ线段的左侧
        /// 返回值 等于 0 , 则P,Q,R三点共线。
        /// </remarks>
        public static Int32 Position(LineI L, PointI R)
        {
            //formular
            //折线段的拐向判断方法可以直接由矢量叉积的性质推出。对于有公共端点的线段PQ和QR，通过计算(R - P) * (Q - P)的符号便可以确定折线段的拐向
            //基本算法是(R-P)计算相对于P点的R点坐标,(Q-P)计算相对于P点的Q点坐标。
            //(R-P) * (Q-P)的计算结果是计算以P为相对原点,点R与点Q的顺时针，逆时针方向。
            //PointI P = L.Starting;
            //PointI Q = L.End;
            //PointI RP = PointAlgorithm.Substract(R, P);//R-P
            //PointI QP = PointAlgorithm.Substract(Q, P);//Q-P
            //return PointAlgorithm.Multiple(RP, QP);
            return PointAlgorithm.Multiple(L.Starting, L.End, R);
        }
        /// <summary>
        /// 判断点与线的位置
        /// </summary>
        /// <param name="P">P点</param>
        /// <param name="Q">Q点</param>
        /// <param name="R">R点</param>
        /// <returns>返回偏转方向</returns>
        /// <remarks>
        /// 假设L线的是 P->Q PQ为线的2个顶点
        /// 返回值 大于 0 , 则PQ在R点拐向右侧后得到QR,等同于点R在PQ线段的右侧
        /// 返回值 小于 0 , 则PQ在R点拐向左侧后得到QR,等同于点R在PQ线段的左侧
        /// 返回值 等于 0 , 则P,Q,R三点共线。
        /// </remarks>
        public static Double Position(PointD P, PointD Q, PointD R)
        {
            //PointD RP = PointAlgorithm.Substract(R, P);//R-P
            //PointD QP = PointAlgorithm.Substract(Q, P);//Q-P
            //return PointAlgorithm.Multiple(RP, QP);
            return PointAlgorithm.Multiple(P, Q, R);
        }
        /// <summary>
        /// 判断点与线的位置
        /// </summary>
        /// <param name="P">P点</param>
        /// <param name="Q">Q点</param>
        /// <param name="R">R点</param>
        /// <returns>返回偏转方向</returns>
        /// <remarks>
        /// 假设L线的是 P->Q PQ为线的2个顶点
        /// 返回值 大于 0 , 则PQ在R点拐向右侧后得到QR,等同于点R在PQ线段的右侧
        /// 返回值 小于 0 , 则PQ在R点拐向左侧后得到QR,等同于点R在PQ线段的左侧
        /// 返回值 等于 0 , 则P,Q,R三点共线。
        /// </remarks>
        public static Int32 Position(PointI P, PointI Q, PointI R)
        {
            //PointI RP = PointAlgorithm.Substract(R, P);//R-P
            //PointI QP = PointAlgorithm.Substract(Q, P);//Q-P
            //return PointAlgorithm.Multiple(RP, QP);
            return PointAlgorithm.Multiple(P, Q, R);
        }
        /// <summary>
        /// 判断点是否在线段的两侧
        /// </summary>
        /// <param name="L">L线</param>
        /// <param name="P1">P1点</param>
        /// <param name="P2">P2点</param>
        /// <returns>返回P1/P2与点的位置关系</returns>
        /// <remarks>
        /// 返回值 小于 0 则点 P1，P2 在线段L的两侧
        /// 返回值 等于 0 则表示点P1,P2至少有一个点与线段L共线
        /// 返回值 大于 0 则点 P1，P2 在线段L的同侧
        /// </remarks>
        public static Double Position(LineD L, PointD P1, PointD P2)
        {
            //则P1P2跨立Q1Q2，即：( P1 - Q1 ) × ( Q2 - Q1 ) *  ( P2 - Q1 ) × ( Q2 - Q1 ) < 0。
            return Position(L, P1) * Position(L, P2);
        }
        /// <summary>
        /// 判断点是否在线段的两侧
        /// </summary>
        /// <param name="L">L线</param>
        /// <param name="P1">P1点</param>
        /// <param name="P2">P2点</param>
        /// <returns>返回P1/P2与点的位置关系</returns>
        /// <remarks>
        /// 返回值 小于 0 则点 P1，P2 在线段L的两侧
        /// 返回值 等于 0 则表示点P1,P2至少有一个点与线段L共线
        /// 返回值 大于 0 则点 P1，P2 在线段L的同侧
        /// </remarks>
        public static Int32 Position(LineI L, PointI P1, PointI P2)
        {
            //则P1P2跨立Q1Q2，即：( P1 - Q1 ) × ( Q2 - Q1 ) *  ( P2 - Q1 ) × ( Q2 - Q1 ) < 0。
            return Position(L, P1) * Position(L, P2);
        }
        #endregion
        #region Gradient
        /// <summary>
        /// 计算线段的倾斜率 
        /// </summary>
        /// <param name="L">线段L</param>
        /// <returns>返回倾斜率</returns>
        public static Double? Gradient(LineD L)
        {
            if (L.Starting.Y == L.End.Y) return 0;
            if (L.Starting.X == L.End.X) return null;
            return (Double)(L.End.Y - L.Starting.Y) / (Double)(L.End.X - L.Starting.X);
        }
        /// <summary>
        /// 计算线段的倾斜率 (-null,null]
        /// </summary>
        /// <param name="L">线段L</param>
        /// <returns>返回倾斜率</returns>
        public static Double? Gradient(LineI L)
        {
            return Gradient((LineD)L);
        }
        #endregion
        #region PointOnLine
        /// <summary>
        /// 判断点是否在线上
        /// </summary>
        /// <param name="L">线L</param>
        /// <param name="P">点P</param>
        /// <returns>返回True表示在线段上，否则返回False。</returns>
        public static Boolean OnLine(LineD L, PointD P)
        {
            //先判断点P是否和线L共线
            if (Position(L, P) == 0)
            {
                MinMax<Double> MX = new MinMax<Double>(L.Starting.X ,L.End.X);
                MinMax<Double> MY = new MinMax<Double>(L.Starting.Y ,L.End.Y);
                //特别要注意的是，由于需要考虑水平线段和垂直线段两种特殊情况，min(xi,xj)<=xk<=max(xi,xj)和min(yi,yj)<=yk<=max(yi,yj)两个条件必须同时满足才能返回真值。
                if(((MX.Min <= P.X) && (P.X <= MX.Max))&& ((MY.Min <= P.Y) &&(P.Y <= MY.Max)))
                    return true;                
            }
            return false;
        }        
        /// <summary>
        /// 判断点是否在线上
        /// </summary>
        /// <param name="L">线L</param>
        /// <param name="P">点P</param>
        /// <returns>返回True表示在线段上，否则返回False。</returns>
        public static Boolean OnLine(LineI L, PointI P)
        {
            //先判断点P是否和线L共线
            if (Position(L, P) == 0)
            {
                MinMax<Int32> MX = new MinMax<Int32>(L.Starting.X, L.End.X);
                MinMax<Int32> MY = new MinMax<Int32>(L.Starting.Y, L.End.Y);
                //特别要注意的是，由于需要考虑水平线段和垂直线段两种特殊情况，min(xi,xj)<=xk<=max(xi,xj)和min(yi,yj)<=yk<=max(yi,yj)两个条件必须同时满足才能返回真值。
                if (((MX.Min <= P.X) && (P.X <= MX.Max)) && ((MY.Min <= P.Y) && (P.Y <= MY.Max)))
                    return true;
            }
            return false;
        }
        #endregion
        #region HasIntersection
        /// <summary>
        /// 判断L1与L2是否相交
        /// </summary>
        /// <param name="L1">线段L1</param>
        /// <param name="L2">线段L2</param>
        /// <returns>线段相交返回交点数目,否则返回0,如果有无数个交点则返回null.</returns>
        public static Int32? HasIntersection(LineD L1, LineD L2)
        {
            //如果两线段相交，则两线段必然相互跨立对方。
            //若P1P2跨立Q1Q2 ，则矢量 ( P1 - Q1 ) 和( P2 - Q1 )位于矢量( Q2 - Q1 ) 的两侧，
            //即( P1 - Q1 ) × ( Q2 - Q1 ) * ( P2 - Q1 ) × ( Q2 - Q1 ) < 0。
            //上式可改写成( P1 - Q1 ) × ( Q2 - Q1 ) * ( Q2 - Q1 ) × ( P2 - Q1 ) > 0。当 ( P1 - Q1 ) × ( Q2 - Q1 ) = 0 时，
            //说明 ( P1 - Q1 ) 和 ( Q2 - Q1 )共线，但是因为已经通过快速排斥试验，所以 P1 一定在线段 Q1Q2上；同理，( Q2 - Q1 ) ×(P2 - Q1 ) = 0 说明 P2 一定在线段 Q1Q2上。
            //所以判断P1P2跨立Q1Q2的依据是：( P1 - Q1 ) × ( Q2 - Q1 ) * ( P2 - Q1 ) × ( Q2 - Q1 )  <= 0。
            //同理判断Q1Q2跨立P1P2的依据是：( Q1 - P1 ) × ( P2 - P1 ) * ( Q2 - P1 ) × ( P2 - P1 )  <= 0。
            if ((Position(L1, L2.Starting, L2.End) == 0) && (Position(L2, L1.Starting, L1.End) == 0))//两线四点 共线
            {
                List<PointD> Points = new List<PointD>();//排除点和线顶点相交的情况
                if (true == OnLine(L1, L2.Starting))
                    Points.Add(L2.Starting);
                if (true == OnLine(L1, L2.End))
                    Points.Add(L2.End);
                if (true == OnLine(L2, L1.Starting))
                    Points.Add(L1.Starting);
                if (true == OnLine(L2, L1.End))
                    Points.Add(L1.End);
                if (Points.Count == 2)
                {
                    if (Points[0].Equals(Points[1]))
                        return 1;
                    return null;
                }
                else if(Points.Count > 2)
                {
                    return null;
                }
                return 0;
            }
            if ((Position(L1, L2.Starting, L2.End) <= 0) && (Position(L2, L1.Starting, L1.End) <= 0))
            {
                return 1;
            }
            return 0;
        }
        /// <summary>
        /// 判断L1与L2是否相交
        /// </summary>
        /// <param name="L1">线段L1</param>
        /// <param name="L2">线段L2</param>
        /// <returns>线段相交返回交点数目,否则返回0,如果有无数个交点则返回null.</returns>
        public static Int32? HasIntersection(LineI L1, LineI L2)
        {
            //如果两线段相交，则两线段必然相互跨立对方。
            //若P1P2跨立Q1Q2 ，则矢量 ( P1 - Q1 ) 和( P2 - Q1 )位于矢量( Q2 - Q1 ) 的两侧，
            //即( P1 - Q1 ) × ( Q2 - Q1 ) * ( P2 - Q1 ) × ( Q2 - Q1 ) < 0。
            //上式可改写成( P1 - Q1 ) × ( Q2 - Q1 ) * ( Q2 - Q1 ) × ( P2 - Q1 ) > 0。当 ( P1 - Q1 ) × ( Q2 - Q1 ) = 0 时，
            //说明 ( P1 - Q1 ) 和 ( Q2 - Q1 )共线，但是因为已经通过快速排斥试验，所以 P1 一定在线段 Q1Q2上；同理，( Q2 - Q1 ) ×(P2 - Q1 ) = 0 说明 P2 一定在线段 Q1Q2上。
            //所以判断P1P2跨立Q1Q2的依据是：( P1 - Q1 ) × ( Q2 - Q1 ) * ( P2 - Q1 ) × ( Q2 - Q1 )  <= 0。
            //同理判断Q1Q2跨立P1P2的依据是：( Q1 - P1 ) × ( P2 - P1 ) * ( Q2 - P1 ) × ( P2 - P1 )  <= 0。

            if ((Position(L1, L2.Starting, L2.End) == 0) && (Position(L2, L1.Starting, L1.End) == 0))//两线四点 共线
            {
                List<PointI> Points = new List<PointI>();//排除点和线顶点相交的情况
                if (true == OnLine(L1, L2.Starting))
                    Points.Add(L2.Starting);
                if (true == OnLine(L1, L2.End))
                    Points.Add(L2.End);
                if (true == OnLine(L2, L1.Starting))
                    Points.Add(L1.Starting);
                if (true == OnLine(L2, L1.End))
                    Points.Add(L1.End);
                if (Points.Count == 2)
                {
                    if (Points[0].Equals(Points[1]))
                        return 1;
                    return null;
                }
                else if (Points.Count > 2)
                {
                    return null;
                }
                return 0;
            }
            if ((Position(L1, L2.Starting, L2.End) <= 0) && (Position(L2, L1.Starting, L1.End) <= 0))
            {
                return 1;
            }
            return 0;
        }
        /// <summary>
        /// 判断线段与矩形的交点个数
        /// </summary>
        /// <param name="L">线段L</param>
        /// <param name="R">线段R</param>
        /// <returns>相交返回交点数目,否则返回0,如果有无数个交点则返回null</returns>
        public static Int32? HasIntersection(LineD L, RectangleD R)
        {
            Int32 count = 0;
            foreach (LineD S in R)
            {
                Int32? result = HasIntersection(L, S);
                if (result == null) return null;
                count += (Int32)result;
            }
            foreach (PointD P in R.GetVertexs())
            {
                if (OnLine(L, P) == true)
                {
                    count--;
                }
            }
            return count;
        }
        /// <summary>
        /// 判断线段与矩形的交点个数
        /// </summary>
        /// <param name="L">线段L</param>
        /// <param name="R">线段R</param>
        /// <returns>相交返回交点数目,否则返回0,如果有无数个交点则返回null</returns>
        public static Int32? HasIntersection(LineI L, RectangleI R)
        {
            Int32 count = 0;
            foreach (LineI S in R)
            {
                Int32? result = HasIntersection(L, S);
                if (result == null) return null;
                count += (Int32)result;
            }
            foreach (PointI P in R.GetVertexs())
            {
                if (OnLine(L, P) == true)
                {
                    count--;
                }
            }
            return count;
        }
        /// <summary>
        /// 判断折线PL与线段L的交点个数
        /// </summary>
        /// <param name="L">线段L</param>
        /// <param name="PL">折线PL</param>
        /// <returns>相交返回交点数目,否则返回0</returns>
        public static Int32? HasIntersection(LineD L, PolylineD PL)
        {
            Int32 count = 0;
            foreach (LineD S in PL)
            {
                Int32? result = HasIntersection(L, S);
                if (result == null) return null;
                count += (Int32)result;
            }
            for (Int32 i = 1; i < PL.Points.Count - 1; ++i)//排除折线的开始和结束顶点
            {
                if (OnLine(L, PL.Points[i]) == true)
                {
                    count--;
                }
            }
            return count;
        }
        /// <summary>
        /// 判断折线PL与线段L的交点个数
        /// </summary>
        /// <param name="L">线段L</param>
        /// <param name="PL">折线PL</param>
        /// <returns>相交返回交点数目,否则返回0</returns>
        public static Int32? HasIntersection(LineI L, PolylineI PL)
        {
            Int32 count = 0;
            foreach (LineI S in PL)
            {
                Int32? result = HasIntersection(L, S);
                if (result == null) return null;
                count += (Int32)result;
            }
            for (Int32 i = 1; i < PL.Points.Count - 1; ++i)//排除折线的开始和结束顶点
            {
                if (OnLine(L, PL.Points[i]) == true)
                {
                    count--;
                }
            }
            return count;
        }
        /// <summary>
        /// 判断多边形PG与线段L的交点个数
        /// </summary>
        /// <param name="L">线段L</param>
        /// <param name="PG">多边形PG</param>
        /// <returns>相交返回交点数目,否则返回0</returns>
        public static Int32? HasIntersection(LineD L, PolygonD PG)
        {
            Int32 count = 0;
            foreach (LineD S in PG)
            {
               Int32? result = HasIntersection(L, S);
                if (result == null) return null;
                count += (Int32)result;
            }
            foreach (PointD P in PG.Vertex)
            {
                if (OnLine(L, P) == true)
                {
                    count--;
                }
            }
            return count;
        }
        /// <summary>
        /// 判断多边形PG与线段L的交点个数
        /// </summary>
        /// <param name="L">线段L</param>
        /// <param name="PG">多边形PG</param>
        /// <returns>相交返回交点数目,否则返回0</returns>
        public static Int32? HasIntersection(LineI L, PolygonI PG)
        {
            Int32 count = 0;
            foreach (LineI S in PG)
            {
                Int32? result = HasIntersection(L, S);
                if (result == null) return null;
                count += (Int32)result;
            }
            foreach (PointI P in PG.Vertex)
            {
                if (OnLine(L, P) == true)
                {
                    count--;
                }
            }
            return count;
        }
        /// <summary>
        /// 判断线段L与圆C的交点个数
        /// </summary>
        /// <param name="L">线段L</param>
        /// <param name="C">圆形C</param>
        /// <returns>相交返回交点数目,否则返回0</returns>
        public static Int32? HasIntersection(LineD L, CircleD C)
        {
            Int32 count = 0;
            //如果和圆C有交点首先是L到圆心的距离小于或等于C的半径
            if (DoubleAlgorithm.Equals(PointAlgorithm.ClosestDistance(C.Center, L), C.Radius)) return 1;
            else if (PointAlgorithm.ClosestDistance(C.Center, L) > C.Radius) return 0;         
            if (PointAlgorithm.Distance(C.Center, L.Starting) >= C.Radius) ++count;
            if (PointAlgorithm.Distance(C.Center, L.End) >= C.Radius) ++count;
            return count;
        }
        /// <summary>
        /// 判断线段L与圆C的交点个数
        /// </summary>
        /// <param name="L">线段L</param>
        /// <param name="C">圆形C</param>
        /// <returns>相交返回交点数目,否则返回0</returns>
        public static Int32? HasIntersection(LineI L, CircleI C)
        {
            Int32 count = 0;
            //如果和圆C有交点首先是L到圆心的距离小于或等于C的半径
            if (DoubleAlgorithm.Equals(PointAlgorithm.ClosestDistance(C.Center, L), C.Radius)) return 1;
            else if (PointAlgorithm.ClosestDistance(C.Center, L) > C.Radius) return 0;
            if (PointAlgorithm.Distance(C.Center, L.Starting) >= C.Radius) ++count;
            if (PointAlgorithm.Distance(C.Center, L.End) >= C.Radius) ++count;
            return count;
        }
        #endregion
        #region Intersection
        /// <summary>
        /// 获取两线段的交点
        /// </summary>
        /// <param name="L1">线段L1</param>
        /// <param name="L2">线段L2</param>
        /// <returns>如果线段相交返回交点，如果有无数个交点则返回null.</returns>
        public static PointD[] Intersection(LineD L1, LineD L2)
        {
            //如果该线段平行于X轴（Y轴），则过点point作该线段所在直线的垂线，垂足很容易求得，然后计算出垂足，如果垂足在线段上则返回垂足，否则返回离垂足近的端点；
            //如果该线段不平行于X轴也不平行于Y轴，则斜率存在且不为0。设线段的两端点为pt1和pt2，斜率为：k = ( pt2.y - pt1. y ) / (pt2.x - pt1.x );
            //该直线方程为：y = k* ( x - pt1.x) + pt1.y。其垂线的斜率为 - 1 / k，垂线方程为：y = (-1/k) * (x - point.x) + point.y 。
            //联立两直线方程解得：x = ( k^2 * pt1.x + k * (point.y - pt1.y ) + point.x ) / ( k^2 + 1) ，y = k * ( x - pt1.x) + pt1.y;然后再判断垂足是否在线段上，
            //如果在线段上则返回垂足；如果不在则计算两端点到垂足的距离，选择距离垂足较近的端点返回。 
            List<PointD> result = new List<PointD>();
            Int32? has = HasIntersection(L1, L2);
            if (has == 0) return result.ToArray();
            if (has == null) return null;
            PointD P = new PointD();
            Double A1 = L1.End.Y - L1.Starting.Y;
            Double B1 = L1.Starting.X - L1.End.X;
            Double C1 = L1.End.X * L1.Starting.Y - L1.Starting.X * L1.End.Y;

            Double A2 = L2.End.Y - L2.Starting.Y;
            Double B2 = L2.Starting.X - L2.End.X;
            Double C2 = L2.End.X * L2.Starting.Y - L2.Starting.X * L2.End.Y;

            if ((Position(L1, L2.Starting, L2.End) == 0) && (Position(L2, L1.Starting, L1.End) == 0))//共线
            {
                List<PointD> Points = new List<PointD>();//排除线和线顶点相交的情况
                if (true == OnLine(L1, L2.Starting))
                    Points.Add(L2.Starting);
                if (true == OnLine(L1, L2.End))
                    Points.Add(L2.End);
                if (true == OnLine(L2, L1.Starting))
                    Points.Add(L1.Starting);
                if (true == OnLine(L2, L1.End))
                    Points.Add(L1.End);
                if (Points.Count == 2)
                {
                    if (Points[0].Equals(Points[1]))
                    {
                        result.Add(Points[0]);
                        return result.ToArray();
                    }
                    return null;
                }
                return result.ToArray();
            }
            P.X = (Double)(B2 * C1 - B1 * C2) / (Double)(A2 * B1 - A1 * B2);
            P.Y = (Double)(A1 * C2 - A2 * C1) / (Double)(A2 * B1 - A1 * B2);
            result.Add(P);
            return result.ToArray();
        }
        /// <summary>
        /// 获取两线段的交点
        /// </summary>
        /// <param name="L1">线段L1</param>
        /// <param name="L2">线段L2</param>
        /// <returns>如果线段相交返回交点，如果有无数个交点则返回null.</returns>
        public static PointD [] Intersection(LineI L1, LineI L2)
        {
            //如果该线段平行于X轴（Y轴），则过点point作该线段所在直线的垂线，垂足很容易求得，然后计算出垂足，如果垂足在线段上则返回垂足，否则返回离垂足近的端点；
            //如果该线段不平行于X轴也不平行于Y轴，则斜率存在且不为0。设线段的两端点为pt1和pt2，斜率为：k = ( pt2.y - pt1. y ) / (pt2.x - pt1.x );
            //该直线方程为：y = k* ( x - pt1.x) + pt1.y。其垂线的斜率为 - 1 / k，垂线方程为：y = (-1/k) * (x - point.x) + point.y 。
            //联立两直线方程解得：x = ( k^2 * pt1.x + k * (point.y - pt1.y ) + point.x ) / ( k^2 + 1) ，y = k * ( x - pt1.x) + pt1.y;然后再判断垂足是否在线段上，
            //如果在线段上则返回垂足；如果不在则计算两端点到垂足的距离，选择距离垂足较近的端点返回。 
            List<PointD> result = new List<PointD>();
            Int32? has = HasIntersection(L1, L2);
            if (has == 0) return result.ToArray();
            if (has == null) return null;
            PointD P = new PointD();
            Double A1 = L1.End.Y - L1.Starting.Y;
            Double B1 = L1.Starting.X - L1.End.X;
            Double C1 = L1.End.X * L1.Starting.Y - L1.Starting.X * L1.End.Y;

            Double A2 = L2.End.Y - L2.Starting.Y;
            Double B2 = L2.Starting.X - L2.End.X;
            Double C2 = L2.End.X * L2.Starting.Y - L2.Starting.X * L2.End.Y;

            if ((Position(L1, L2.Starting, L2.End) == 0) && (Position(L2, L1.Starting, L1.End) == 0))//共线
            {
                List<PointD> Points = new List<PointD>();//排除线和线顶点相交的情况
                if (true == OnLine(L1, L2.Starting))
                    Points.Add(L2.Starting);
                if (true == OnLine(L1, L2.End))
                    Points.Add(L2.End);
                if (true == OnLine(L2, L1.Starting))
                    Points.Add(L1.Starting);
                if (true == OnLine(L2, L1.End))
                    Points.Add(L1.End);
                if (Points.Count == 2)
                {
                    if (Points[0].Equals(Points[1]))
                    {
                        result.Add(Points[0]);
                        return result.ToArray();
                    }
                    return null;
                } 
                return result.ToArray();
            }
            P.X = (Double)(B2 * C1 - B1 * C2) / (Double)(A2 * B1 - A1 * B2);
            P.Y = (Double)(A1 * C2 - A2 * C1) / (Double)(A2 * B1 - A1 * B2);
            result.Add(P);
            return result.ToArray();
        }
        /// <summary>
        /// 获取线段L与矩形R的交点集合
        /// </summary>
        /// <param name="L">线段L</param>
        /// <param name="R">矩形R</param>
        /// <returns>返回交点集合，如果有无数个交点则返回null.</returns>
        public static PointD[] Intersection(LineD L, RectangleD R)
        {
            List<PointD> result = new List<PointD>();
            foreach (LineD S in R)
            {
                PointD[] P = Intersection(L, S);
                if (P == null) return null;
                if (P.Length == 0) continue;
                if(result.Contains(P[0])==false)
                    result.Add((PointD)P[0]);
            }
            return result.ToArray();
        }

        /// <summary>
        /// 获取线段L与矩形R的交点集合
        /// </summary>
        /// <param name="L">线段L</param>
        /// <param name="R">矩形R</param>
        /// <returns>返回交点集合，如果有无数个交点则返回null.</returns>
        public static PointD[] Intersection(LineI L, RectangleI R)
        {
            List<PointD> result = new List<PointD>();
            foreach (LineI S in R)
            {
                PointD[] P = Intersection(L, S);
                if (P == null) return null;
                if (P.Length == 0) continue;
                result.Add((PointD)P[0]);
                if (result.Contains(P[0]) == false)
                    result.Add((PointD)P[0]);
            }
            return result.ToArray();
        }

        /// <summary>
        /// 获取线段L与折线PL的交点集合
        /// </summary>
        /// <param name="L">线段L</param>
        /// <param name="PL">折线PL</param>
        /// <returns>返回交点集合，如果有无数个交点则返回null.</returns>
        public static PointD[] Intersection(LineD L, PolylineD PL)
        {
            List<PointD> result = new List<PointD>();
            foreach (LineD S in PL)
            {
                PointD[] P = Intersection(L, S);
                if (P == null) return null;
                if (P.Length == 0) continue; 
                if (result.Contains(P[0]) == false)
                    result.Add((PointD)P[0]);
            }
            return result.ToArray();
        }

        /// <summary>
        /// 获取线段L与折线PL的交点集合
        /// </summary>
        /// <param name="L">线段L</param>
        /// <param name="PL">折线PL</param>
        /// <returns>返回交点集合，如果有无数个交点则返回null.</returns>
        public static PointD[] Intersection(LineI L, PolylineI PL)
        {
            List<PointD> result = new List<PointD>();
            foreach (LineI S in PL)
            {
                PointD[] P = Intersection(L, S);
                if (P == null) return null;
                if (P.Length == 0) continue;
                if (result.Contains(P[0]) == false)
                    result.Add((PointD)P[0]);
            }
            return result.ToArray();
        }

        /// <summary>
        /// 获取线段L与多边形PG的交点集合
        /// </summary>
        /// <param name="L">线段L</param>
        /// <param name="PG">多边形PG</param>
        /// <returns>返回交点集合，如果有无数个交点则返回null.</returns>
        public static PointD[] Intersection(LineD L, PolygonD PG)
        {
            List<PointD> result = new List<PointD>();
            foreach (LineD S in PG)
            {
                PointD[] P = Intersection(L, S);
                if (P == null) return null;
                if (P.Length == 0) continue;
                if (result.Contains(P[0]) == false)
                    result.Add((PointD)P[0]);
            }
            return result.ToArray();
        }
        /// <summary>
        /// 获取线段L与多边形PG的交点集合
        /// </summary>
        /// <param name="L">线段L</param>
        /// <param name="PG">多边形PG</param>
        /// <returns>返回交点集合，如果有无数个交点则返回null.</returns>
        public static PointD[] Intersection(LineI L, PolygonI PG)
        {
            List<PointD> result = new List<PointD>();
            foreach (LineI S in PG)
            {
                PointD[] P = Intersection(L, S);
                if (P == null) return null;
                if (P.Length == 0) continue;
                if (result.Contains(P[0]) == false)
                    result.Add((PointD)P[0]);
            }
            return result.ToArray();
        }


        /// <summary>
        /// 获取线段L与圆C的交点集合
        /// </summary>
        /// <param name="L">线段L</param>
        /// <param name="C">圆C</param>
        /// <returns>返回交点集合.</returns>
        public static PointD[] Intersection(LineD L, CircleD C)
        {
            List<PointD> result = new List<PointD>();
            Int32? has = HasIntersection(L, C);
            if (has == 0 || has == null) return result.ToArray();
            
            //Points P (x,y) on a line defined by two points P1 (x1,y1,z1) and P2 (x2,y2,z2) is described by
            //P = P1 + u (P2 - P1)

            //or in each coordinate
            //x = x1 + u (x2 - x1)
            //y = y1 + u (y2 - y1)
            //z = z1 + u (z2 - z1)

            //A sphere centered at P3 (x3,y3,z3) with radius r is described by
            //(x - x3)2 + (y - y3)2 + (z - z3)2 = r2

            //Substituting the equation of the line into the sphere gives a quadratic equation of the form
            //a u2 + b u + c = 0

            //where:
            //a = (x2 - x1)2 + (y2 - y1)2 + (z2 - z1)2

            //b = 2[ (x2 - x1) (x1 - x3) + (y2 - y1) (y1 - y3) + (z2 - z1) (z1 - z3) ]

            //c = x32 + y32 + z32 + x12 + y12 + z12 - 2[x3 x1 + y3 y1 + z3 z1] - r2


            //The solutions to this quadratic are described by
            PointD PD = PointAlgorithm.Substract(L.Starting, L.End);
            Double a = PD.X * PD.X + PD.Y * PD.Y;
            Double b = 2 * ((L.End.X - L.Starting.X) * (L.Starting.X - C.Center.X) + (L.End.Y - L.Starting.Y) * (L.Starting.Y - C.Center.Y));
            Double c = C.Center.X * C.Center.X + C.Center.Y * C.Center.Y + L.Starting.X * L.Starting.X + L.Starting.Y * L.Starting.Y - 2 * (C.Center.X * L.Starting.X + C.Center.Y * L.Starting.Y) - C.Radius * C.Radius;
            Double u1 = ((-1) * b + System.Math.Sqrt(b * b - 4 * a * c)) / (2 * a);
            Double u2 = ((-1) * b - System.Math.Sqrt(b * b - 4 * a * c)) / (2 * a);
            //交点
            PointD P1 = new PointD(L.Starting.X + u1 * (L.End.X - L.Starting.X), L.Starting.Y + u1 * (L.End.Y - L.Starting.Y));
            PointD P2 = new PointD(L.Starting.X + u2 * (L.End.X - L.Starting.X), L.Starting.Y + u2 * (L.End.Y - L.Starting.Y));
            if (LineAlgorithm.OnLine(L, P1) == true)
                result.Add(P1);
            if (LineAlgorithm.OnLine(L, P2) == true && P1.Equals(P2)==false)
                result.Add(P2);
            return result.ToArray();
        }
        /// <summary>
        /// 获取线段L与圆C的交点集合
        /// </summary>
        /// <param name="L">线段L</param>
        /// <param name="C">圆C</param>
        /// <returns>返回交点集合.</returns>
        public static PointD[] Intersection(LineI L, CircleI C)
        {
            List<PointD> result = new List<PointD>();
            Int32? has = HasIntersection(L, C);
            if (has == 0 || has == null) return result.ToArray();

            //Points P (x,y) on a line defined by two points P1 (x1,y1,z1) and P2 (x2,y2,z2) is described by
            //P = P1 + u (P2 - P1)

            //or in each coordinate
            //x = x1 + u (x2 - x1)
            //y = y1 + u (y2 - y1)
            //z = z1 + u (z2 - z1)

            //A sphere centered at P3 (x3,y3,z3) with radius r is described by
            //(x - x3)2 + (y - y3)2 + (z - z3)2 = r2

            //Substituting the equation of the line into the sphere gives a quadratic equation of the form
            //a u2 + b u + c = 0

            //where:
            //a = (x2 - x1)2 + (y2 - y1)2 + (z2 - z1)2

            //b = 2[ (x2 - x1) (x1 - x3) + (y2 - y1) (y1 - y3) + (z2 - z1) (z1 - z3) ]

            //c = x32 + y32 + z32 + x12 + y12 + z12 - 2[x3 x1 + y3 y1 + z3 z1] - r2


            //The solutions to this quadratic are described by
            PointD PD = PointAlgorithm.Substract(L.Starting, L.End);
            Double a = PD.X * PD.X + PD.Y * PD.Y;
            Double b = 2 * ((L.End.X - L.Starting.X) * (L.Starting.X - C.Center.X) + (L.End.Y - L.Starting.Y) * (L.Starting.Y - C.Center.Y));
            Double c = C.Center.X * C.Center.X + C.Center.Y * C.Center.Y + L.Starting.X * L.Starting.X + L.Starting.Y * L.Starting.Y - 2 * (C.Center.X * L.Starting.X + C.Center.Y * L.Starting.Y) - C.Radius * C.Radius;
            Double u1 = ((-1) * b + System.Math.Sqrt(b * b - 4 * a * c)) / (2 * a);
            Double u2 = ((-1) * b - System.Math.Sqrt(b * b - 4 * a * c)) / (2 * a);
            //交点
            PointD P1 = new PointD(L.Starting.X + u1 * (L.End.X - L.Starting.X), L.Starting.Y + u1 * (L.End.Y - L.Starting.Y));
            PointD P2 = new PointD(L.Starting.X + u2 * (L.End.X - L.Starting.X), L.Starting.Y + u2 * (L.End.Y - L.Starting.Y));
            if (LineAlgorithm.OnLine(L, P1) == true)
                result.Add(P1);
            if (LineAlgorithm.OnLine(L, P2) == true && P1.Equals(P2) == false)
                result.Add(P2);
            return result.ToArray();
        }
        #endregion
        #region Cos
        #endregion
        #region Offset
        /// <summary>
        /// 计算偏移后的线段
        /// </summary>
        /// <param name="L">线段L</param>
        /// <param name="velocity">偏移速度</param>
        /// <returns>返回偏移后的线段</returns>
        public static LineD Offset(LineD L, PointD velocity)
        {
            return new LineD(L.Starting + velocity, L.End + velocity);
        }
        /// <summary>
        /// 计算偏移后的线段
        /// </summary>
        /// <param name="L">线段L</param>
        /// <param name="velocity">偏移速度</param>
        /// <returns>返回偏移后的线段</returns>
        public static LineI Offset(LineI L, PointI velocity)
        {
            return new LineI(L.Starting + velocity, L.End + velocity);
        }
        #endregion
        #region Included Angle
        /// <summary>
        /// 计算线段L与X轴的夹角
        /// </summary>
        /// <param name="L">线段L</param>
        /// <returns>返回夹角弧度</returns>
        public static Double IncludedAngle(LineD L)
        {
            return PointAlgorithm.Angle(L.Starting, L.End, new PointD(10000000, L.Starting.Y));
        }
        /// <summary>
        /// 计算线段L与X轴的夹角
        /// </summary>
        /// <param name="L">线段L</param>
        /// <returns>返回夹角弧度</returns>
        public static Double IncludedAngle(LineI L)
        {
            return IncludedAngle((LineD)L);
        }
        /// <summary>
        /// 计算两线段所在直线的夹角
        /// </summary>
        /// <param name="L1">线段L1</param>
        /// <param name="L2">线段L2</param>
        /// <returns>返回夹角</returns>
        public static Double IncludedAngle(LineD L1,LineD L2)
        {
            Double a1 = IncludedAngle(L1);
            Double a2 = IncludedAngle(L2);
            return (a2-a1) * 180/System.Math.PI;

        }        
        /// <summary>
        /// 计算两线段所在直线的夹角
        /// </summary>
        /// <param name="L1">线段L1</param>
        /// <param name="L2">线段L2</param>
        /// <returns>返回夹角</returns>
        public static Double IncludedAngle(LineI L1, LineI L2)
        {
            return IncludedAngle((LineD)L1, (LineD)L2);
        }
        #endregion
    }
}
