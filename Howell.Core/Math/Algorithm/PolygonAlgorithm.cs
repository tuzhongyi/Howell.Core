using System;
using System.Collections.Generic;
using System.Text;
using Howell.Drawing.D2;

namespace Howell.Math.Algorithm
{
    /// <summary>
    /// 多边形算法
    /// </summary>
    public static class PolygonAlgorithm
    {
        #region InPolygon
        /// <summary>
        /// 点P是否在多边形区域内
        /// </summary>
        /// <param name="PG">多边形PL</param>
        /// <param name="P">点P</param>
        /// <returns>如果点P在区域内返回True,否则返回False.</returns>
        /// <remarks>此处不考虑平行边的情况</remarks>
        public static Boolean InPolygon(PolygonD PG, PointD P)
        {
            //count ← 0;
            //以P为端点，作从右向左的射线L; 
            //for 多边形的每条边s
            // do if P在边s上 
            //      then return true;
            //    if s不是水平的
            //      then if s的一个端点在L上
            //             if 该端点是s两端点中纵坐标较大的端点
            //               then count ← count+1
            //           else if s和L相交
            //             then count ← count+1;
            //if count mod 2 = 1 
            //  then return true;
            //else return false;
            Int32 count = 0;
            LineD L = new LineD(P, new PointD(Double.MaxValue, P.Y));
            foreach (LineD S in PG)
            {
                if (LineAlgorithm.OnLine(S, P) == true) return true;
                if (LineAlgorithm.Gradient(S) != 0)//不是水平线段
                {
                    if (LineAlgorithm.OnLine(L, S.Starting) || LineAlgorithm.OnLine(L, S.End))
                    {
                        if(LineAlgorithm.OnLine(L,S.Starting) && S.Starting.Y > S.End.Y)
                            ++count;
                        if (LineAlgorithm.OnLine(L, S.End) && S.End.Y > S.Starting.Y)
                            ++count;
                    }
                    else if (LineAlgorithm.HasIntersection(S, L) > 0)
                    {
                        ++count;
                    }
                }

            }
            //如果X的水平射线和多边形的交点数是奇数个则点在多边形内，否则不在区域内。
            if (count % 2 == 0) return false;
            return true;
        }
        /// <summary>
        /// 点P是否在多边形区域内
        /// </summary>
        /// <param name="PG">多边形PL</param>
        /// <param name="P">点P</param>
        /// <returns>如果点P在区域内返回True,否则返回False.</returns>
        /// <remarks>此处不考虑平行边的情况</remarks>
        public static Boolean InPolygon(PolygonI PG, PointI P)
        {
            //count ← 0;
            //以P为端点，作从右向左的射线L; 
            //for 多边形的每条边s
            // do if P在边s上 
            //      then return true;
            //    if s不是水平的
            //      then if s的一个端点在L上
            //             if 该端点是s两端点中纵坐标较大的端点
            //               then count ← count+1
            //           else if s和L相交
            //             then count ← count+1;
            //if count mod 2 = 1 
            //  then return true;
            //else return false;
            Int32 count = 0;
            LineI L = new LineI(P, new PointI(Int32.MaxValue, P.Y));
            foreach (LineI S in PG)
            {
                if (LineAlgorithm.OnLine(S, P) == true) return true;
                if (LineAlgorithm.Gradient(S) != 0)//不是水平线段
                {
                    if (LineAlgorithm.OnLine(L, S.Starting) || LineAlgorithm.OnLine(L, S.End))
                    {
                        if (LineAlgorithm.OnLine(L, S.Starting) && S.Starting.Y > S.End.Y)
                            ++count;
                        if (LineAlgorithm.OnLine(L, S.End) && S.End.Y > S.Starting.Y)
                            ++count;
                    }
                    else if (LineAlgorithm.HasIntersection(S, L) > 0)
                    {
                        ++count;
                    }
                }
            }
            //如果X的水平射线和多边形的交点数是奇数个则点在多边形内，否则不在区域内。
            if (count % 2 == 0) return false;
            return true;
        }
        /// <summary>
        /// 线段L是否在多边形区域内
        /// </summary>
        /// <param name="PG">多边形PG</param>
        /// <param name="L">线段L</param>
        /// <returns>如果线段L在区域内返回True,否则返回False.</returns>
        public static Boolean InPolygon(PolygonD PG, LineD L)
        {
            //if 线端PQ的端点不都在多边形内 
            //  then return false;
            //点集pointSet初始化为空;
            //for 多边形的每条边s
            //  do if 线段的某个端点在s上
            //       then 将该端点加入pointSet;
            //     else if s的某个端点在线段PQ上
            //       then 将该端点加入pointSet;
            //     else if s和线段PQ相交 // 这时候已经可以肯定是内交了
            //       then return false;
            //将pointSet中的点按照X-Y坐标排序;
            //for pointSet中每两个相邻点 pointSet[i] , pointSet[ i+1]
            //  do if pointSet[i] , pointSet[ i+1] 的中点不在多边形中
            //       then return false;
            //return true;
            List<PointD> PointList = new List<PointD>();
            if (InPolygon(PG, L.Starting) == false) return false;
            foreach (LineD S in PG)
            {
                if (LineAlgorithm.OnLine(S, L.Starting) == true)
                    PointList.Add(L.Starting);
                else if (LineAlgorithm.OnLine(S, L.End) == true)
                    PointList.Add(L.End);
                else if (LineAlgorithm.OnLine(L, S.Starting) == true)
                    PointList.Add(S.Starting);
                else if (LineAlgorithm.OnLine(L, S.End) == true)
                    PointList.Add(S.End);
                else if (LineAlgorithm.HasIntersection(L, S) > 0)
                    return false;
            }
            PointD[] OrderedPointList = PointList.ToArray();
            for (Int32 i = 0; i < (OrderedPointList.Length -1); ++i)
            {
                for (Int32 j = 0; j < (OrderedPointList.Length - i - 1); j++)
                {
                    MinMax<PointD> MM = new MinMax<PointD>(OrderedPointList[j], OrderedPointList[j + 1]);
                    OrderedPointList[j] = MM.Min;
                    OrderedPointList[j + 1] = MM.Max;
                }
            }
            for (Int32 i = 0; i < (OrderedPointList.Length - 1); ++i)
            {
                if (false == InPolygon(PG, PointAlgorithm.MidPoint(OrderedPointList[i], OrderedPointList[i + 1])))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// 线段L是否在多边形区域内
        /// </summary>
        /// <param name="PG">多边形PG</param>
        /// <param name="L">线段L</param>
        /// <returns>如果线段L在区域内返回True,否则返回False.</returns>
        public static Boolean InPolygon(PolygonI PG, LineI L)
        {
            //if 线端PQ的端点不都在多边形内 
            //  then return false;
            //点集pointSet初始化为空;
            //for 多边形的每条边s
            //  do if 线段的某个端点在s上
            //       then 将该端点加入pointSet;
            //     else if s的某个端点在线段PQ上
            //       then 将该端点加入pointSet;
            //     else if s和线段PQ相交 // 这时候已经可以肯定是内交了
            //       then return false;
            //将pointSet中的点按照X-Y坐标排序;
            //for pointSet中每两个相邻点 pointSet[i] , pointSet[ i+1]
            //  do if pointSet[i] , pointSet[ i+1] 的中点不在多边形中
            //       then return false;
            //return true;
            List<PointI> PointList = new List<PointI>();
            if (InPolygon(PG, L.Starting) == false) return false;
            foreach (LineI S in PG)
            {
                if (LineAlgorithm.OnLine(S, L.Starting) == true)
                    PointList.Add(L.Starting);
                else if (LineAlgorithm.OnLine(S, L.End) == true)
                    PointList.Add(L.End);
                else if (LineAlgorithm.OnLine(L, S.Starting) == true)
                    PointList.Add(S.Starting);
                else if (LineAlgorithm.OnLine(L, S.End) == true)
                    PointList.Add(S.End);
                else if (LineAlgorithm.HasIntersection(L, S) > 0)
                    return false;
            }
            PointI[] OrderedPointList = PointList.ToArray();
            for (Int32 i = 0; i < (OrderedPointList.Length - 1); ++i)
            {
                for (Int32 j = 0; j < (OrderedPointList.Length - i - 1); j++)
                {
                    MinMax<PointI> MM = new MinMax<PointI>(OrderedPointList[j], OrderedPointList[j + 1]);
                    OrderedPointList[j] = MM.Min;
                    OrderedPointList[j + 1] = MM.Max;
                }
            }
            for (Int32 i = 0; i < (OrderedPointList.Length - 1); ++i)
            {
                if (false == InPolygon(PG, PointAlgorithm.MidPoint(OrderedPointList[i], OrderedPointList[i + 1])))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// 判断折线PL是否在多边形内
        /// </summary>
        /// <param name="PG">PG多边形</param>
        /// <param name="PL">PL折线</param>
        /// <returns>如果PL折线在区域内返回True,否则返回False.</returns>
        public static Boolean InPolygon(PolygonD PG, PolylineD PL)
        {
            foreach (LineD L in PL)
            {
                if (InPolygon(PG, L) == false) return false;
            }
            return true;
        }
        /// <summary>
        /// 判断折线PL是否在多边形内
        /// </summary>
        /// <param name="PG">PG多边形</param>
        /// <param name="PL">PL折线</param>
        /// <returns>如果PL折线在区域内返回True,否则返回False.</returns>
        public static Boolean InPolygon(PolygonI PG, PolylineI PL)
        {
            foreach (LineI L in PL)
            {
                if (InPolygon(PG, L) == false) return false;
            }
            return true;
        }

        /// <summary>
        /// 判断多边形PL是否在多边形内
        /// </summary>
        /// <param name="PG">PG多边形</param>
        /// <param name="PL">PL多边形</param>
        /// <returns>如果PL多边形在区域内返回True,否则返回False.</returns>
        public static Boolean InPolygon(PolygonD PG, PolygonD PL)
        {
            foreach (LineD L in PL)
            {
                if (InPolygon(PG, L) == false) return false;
            }
            return true;
        }
        /// <summary>
        /// 判断多边形PL是否在多边形内
        /// </summary>
        /// <param name="PG">PG多边形</param>
        /// <param name="PL">PL多边形</param>
        /// <returns>如果PL多边形在区域内返回True,否则返回False.</returns>
        public static Boolean InPolygon(PolygonI PG, PolygonI PL)
        {
            foreach (LineI L in PL)
            {
                if (InPolygon(PG, L) == false) return false;
            }
            return true;
        }
        /// <summary>
        /// 判断矩形R是否在多边形内
        /// </summary>
        /// <param name="PG">PG多边形</param>
        /// <param name="R">R矩形</param>
        /// <returns>如果R矩形在区域内返回True,否则返回False.</returns>
        public static Boolean InPolygon(PolygonD PG, RectangleD R)
        {
            foreach (LineD L in R)
            {
                if (InPolygon(PG, L) == false) return false;
            }
            return true;
        }        
        /// <summary>
        /// 判断矩形R是否在多边形内
        /// </summary>
        /// <param name="PG">PG多边形</param>
        /// <param name="R">R矩形</param>
        /// <returns>如果R矩形在区域内返回True,否则返回False.</returns>
        public static Boolean InPolygon(PolygonI PG, RectangleI R)
        {
            foreach (LineD L in R)
            {
                if (InPolygon(PG, L) == false) return false;
            }
            return true;
        }
        /// <summary>
        /// 判断圆C是否在多边形PG内
        /// </summary>
        /// <param name="PG">多边形PG</param>
        /// <param name="C">圆C</param>
        /// <returns>如果圆C在区域内返回True,否则返回False.</returns>
        public static Boolean InPolygon(PolygonD PG, CircleD C)
        {
            //如果圆心不在多边形内则返回不在多边形内
            if (false == InPolygon(PG, C.Center)) return false;
            Double D = PointAlgorithm.ClosestDistance(C.Center, PG);
            if (D > C.Radius || DoubleAlgorithm.Equals(D,C.Radius))
                return true;
            return false;
        }
        /// <summary>
        /// 判断圆C是否在多边形PG内
        /// </summary>
        /// <param name="PG">PG多边形</param>
        /// <param name="C">圆C</param>
        /// <returns>如果圆C在区域内返回True,否则返回False.</returns>
        public static Boolean InPolygon(PolygonI PG, CircleI C)
        {
            //如果圆心不在多边形内则返回不在多边形内
            if (false == InPolygon(PG, C.Center)) return false;
            Double D = PointAlgorithm.ClosestDistance(C.Center, PG);            
            if (D > C.Radius || DoubleAlgorithm.Equals(D, C.Radius))
                return true;
            return false;
        }
        #endregion

        #region IsConvexPolygon
        /// <summary>
        /// 判断是否为凸多边形
        /// </summary>
        /// <param name="PG">PG多边形</param>
        /// <returns>如果是凸多边形返回True，否则返回False。</returns>
        public static Boolean IsConvexPolygon(PolygonD PG)
        {
            if (PG.Vertex == null) return false;
            if (PG.Vertex.Count < 4) return true;
            for (Int32 i = 0; i < PG.Vertex.Count; ++i)
            {
                LineD line = new LineD(PG.Vertex[i], PG.Vertex[(i + 1) % PG.Vertex.Count]);
                Double flag = 0;
                for (Int32 j = 0; j < PG.Vertex.Count; ++j)
                {                    
                    Double result = LineAlgorithm.Position(line, PG.Vertex[j]);
                    if ((flag * result) < 0)//点在线的两侧则返回失败。
                        return false;
                    if(result != 0)
                        flag = result;
                }
            }
            return true;
        }
        /// <summary>
        /// 判断是否为凸多边形
        /// </summary>
        /// <param name="PG">PG多边形</param>
        /// <returns>如果是凸多边形返回True，否则返回False。</returns>
        public static Boolean IsConvexPolygon(PolygonI PG)
        {
            if (PG.Vertex == null) return false;
            if (PG.Vertex.Count < 4) return true;
            for (Int32 i = 0; i < PG.Vertex.Count; ++i)
            {
                LineD line = new LineD(PG.Vertex[i], PG.Vertex[(i + 1) % PG.Vertex.Count]);
                Double flag = 0;
                for (Int32 j = 0; j < PG.Vertex.Count; ++j)
                {
                    Double result = LineAlgorithm.Position(line, PG.Vertex[j]);
                    if ((flag * result) < 0)//点在线的两侧则返回失败。
                        return false;
                    if (result != 0)
                        flag = result;
                }
            }
            return true;
        }
        #endregion

        #region Area
        /// <summary>
        /// 计算多边形PG面积
        /// </summary>
        /// <param name="PG">多边形PG</param>
        /// <returns>返回面积。</returns>
        public static Double Area(PolygonD PG)
        {
            //formula  (1/2) *( (Xi*Yi+1 -Xi+1*Yi) +...)
            Double result = 0;
            for (Int32 i = 0; i < PG.Vertex.Count; ++i)
            {
                result += PointAlgorithm.Multiple(PG.Vertex[i % PG.Vertex.Count], PG.Vertex[(i + 1) % PG.Vertex.Count]);
            }
            return System.Math.Abs(0.5 * result);
        }
        /// <summary>
        /// 计算多边形PG面积
        /// </summary>
        /// <param name="PG">多边形PG</param>
        /// <returns>返回面积。</returns>
        public static Double Area(PolygonI PG)
        {
            //formula  (1/2) *( (Xi*Yi+1 -Xi+1*Yi) +...)
            Double result = 0;
            for (Int32 i = 0; i < PG.Vertex.Count; ++i)
            {
                result += PointAlgorithm.Multiple(PG.Vertex[i % PG.Vertex.Count], PG.Vertex[(i + 1) % PG.Vertex.Count]);
            }
            return System.Math.Abs(0.5 * result);
        }
        #endregion

        #region Offset
        /// <summary>
        /// 计算偏移多边形PG
        /// </summary>
        /// <param name="PG">多边形PG</param>
        /// <param name="velocity">偏移速度</param>
        /// <returns>返回偏移多边形</returns>
        public static PolygonD Offset(PolygonD PG, PointD velocity)
        {
            List<PointD> list = new List<PointD>();
            for (Int32 i = 0; i < PG.Vertex.Count; ++i)
            {
                list.Add(PG.Vertex[i] + velocity);
            }
            return new PolygonD(list.ToArray());
        }
        /// <summary>
        /// 计算偏移多边形PG
        /// </summary>
        /// <param name="PG">多边形PG</param>
        /// <param name="velocity">偏移速度</param>
        /// <returns>返回偏移多边形</returns>
        public static PolygonI Offset(PolygonI PG, PointI velocity)
        {
            List<PointI> list = new List<PointI>();
            for (Int32 i = 0; i < PG.Vertex.Count; ++i)
            {
                list.Add(PG.Vertex[i] + velocity);
            }
            return new PolygonI(list.ToArray());
        }
        #endregion



        #region Validate Polygon
        /// <summary>
        /// 获取有效的多边形
        /// </summary>
        /// <param name="PG">多边形PG</param>
        /// <returns>返回多边形</returns>
        public static PolygonD? GetValidatePolygon(PolygonD PG)
        {
            List<PointD> pts = new List<PointD>();
            for (Int32 i = 0; i < PG.Vertex.Count; ++i)
            {
                if (pts.Contains(PG.Vertex[i]) == false)
                {
                    if (pts.Count > 2)
                    {
                        //斜率相等
                        if (LineAlgorithm.Gradient(new LineD(pts[pts.Count - 2], pts[pts.Count - 1])) == LineAlgorithm.Gradient(new LineD(pts[pts.Count - 1], PG.Vertex[i])))
                        {
                            pts[pts.Count - 1] = PG.Vertex[i];
                        }
                        else
                        {
                            pts.Add(PG.Vertex[i]);
                        }
                    }
                }
            }
            if (pts.Count < 3) return null;
            return new PolygonD(pts.ToArray());
        }
        /// <summary>
        /// 获取有效的多边形
        /// </summary>
        /// <param name="PG">多边形PG</param>
        /// <returns>返回多边形</returns>
        public static PolygonI? GetValidatePolygon(PolygonI PG)
        {
            return (PolygonI)GetValidatePolygon((PolygonD)PG);
        }

        #endregion
    }
}
