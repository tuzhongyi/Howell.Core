using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Howell.Drawing.D2
{
    /// <summary>
    /// 折线
    /// </summary>
    public struct PolylineD : IEquatable<PolylineD>, IEnumerable<LineD>
    {
        /// <summary>
        /// 正则表达式
        /// </summary>
        internal const String RegularExpressionString = @"^\{(Point=(?<Point>\{X=(?<X>[+-]?(?:\d+)(?:\.\d+)?),Y=(?<Y>[+-]?(?:\d+)(?:\.\d+)?)\}),?)+\}$";
        /// <summary>
        /// 空对象
        /// </summary>
        public static readonly PolylineD Empty = new PolylineD(null);


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="points">折线的拐点</param>        
        public PolylineD(IList<PointD> points)
        {
            m_Points = points;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="points">折线的拐点</param>        
        public PolylineD(PointD[] points)
        {
            m_Points = null;
            if (points == null) return;
            m_Points = new List<PointD>();
            for (Int32 i = 0; i < points.Length; ++i)
            {
                m_Points.Add(points[i]);
            }
        }
        private IList<PointD> m_Points;
        /// <summary>
        /// 折线的拐点
        /// </summary>
        public IList<PointD> Points
        {
            get
            {
                return m_Points;
            }
            set
            {
                m_Points = value;
            }
        }


        #region operator
        /// <summary>
        /// 解析多线段的字符串形式
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>如果解析成功，返回PolylineD类型，否则将抛出异常</returns>
        /// <exception cref="System.FormatException">非法的字符串格式。</exception>
        public static PolylineD Parse(String value)
        {
            if (value == "") return new PolylineD(null);
            Regex reg = new Regex(RegularExpressionString, RegexOptions.Compiled);
            if (reg.IsMatch(value) == false) throw new FormatException(String.Format("{0}'s string format is illegal. {1}", typeof(PolylineD).FullName, value));
            MatchCollection collection = reg.Matches(value);
            CaptureCollection vertexs = collection[0].Groups["Point"].Captures;
            List<PointD> points = new List<PointD>();
            for (int i = 0; i < vertexs.Count; ++i)
            {
                points.Add(PointD.Parse(vertexs[i].Value));
            }
            return new PolylineD(points.ToArray());
        }
        /// <summary>
        /// 获取多线段类型的字符串型式
        /// </summary>
        /// <returns>返回多线段类型的字符串型式。</returns>
        public override string ToString()
        {
            String result = "";
            if (m_Points != null)
            {
                result += "{";
                for (int i = 0; i < m_Points.Count; ++i)
                {
                    if (i > 0) result += ",";
                    result += String.Format("Point={0}", m_Points[i].ToString());
                }
                result += "}";
            }
            return result;
        }
        /// <summary>
        /// Check if this instance of <see cref="PolygonD"/> equal to the specified one.
        /// </summary>
        /// <param name="obj">Another point to check equalty to.</param>
        /// <returns>Return <see langword="true"/> if objects are equal.</returns>
        public override bool Equals(object obj)
        {
            return (obj is PolylineD) ? (this == (PolylineD)obj) : false;
        }
        /// <summary>
        /// Get hash code for this instance.
        /// </summary>
        /// <returns>Returns the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        /// <summary>
        /// equals operator - checks if two polygon is the same.
        /// </summary>
        /// <param name="p1">PolylineD object 1.</param>
        /// <param name="p2">PolylineD object 2.</param>
        /// <returns>true/false</returns>
        public static bool operator ==(PolylineD p1, PolylineD p2)
        {
            if (p1.Points == null && p2.Points == null) return true;
            if (p1.Points == null || p2.Points == null) return false;
            if (p1.Points.Count != p2.Points.Count) return false;
            for (Int32 i = 0; i < p1.Points.Count; ++i)
            {
                if (p1.Points[i].Equals(p2.Points[i]) == false) return false;
            }
            return true;
        }

        /// <summary>
        /// equals operator - checks if two polygon is not the same.
        /// </summary>
        /// <param name="p1">PolylineD object 1.</param>
        /// <param name="p2">PolylineD object 2.</param>
        /// <returns>true/false</returns>
        public static bool operator !=(PolylineD p1, PolylineD p2)
        {
            return !(p1 == p2);
        }


        /// <summary>
        /// 显式转换PolylineD To PolygonI
        /// </summary>
        /// <param name="polyline">折线</param>
        /// <returns></returns>
        public static explicit operator PolylineI(PolylineD polyline)
        {
            PolylineI result = PolylineI.Empty;
            if (polyline.IsEmpty) return result;
            result.Points = new List<PointI>();
            for (Int32 i = 0; i < polyline.Points.Count; ++i)
            {
                result.Points.Add((PointI)polyline.Points[i]);
            }
            return result;
        }
        #endregion
        /// <summary>
        /// 是否为空
        /// </summary>
        public Boolean IsEmpty
        {
            get
            {
                return (this == PolylineD.Empty);
            }
        }
        #region IEquatable<PolylineD> 成员
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(PolylineD other)
        {
            return (this == other);
        }

        #endregion

        #region IEnumerable<LineD> 成员
        /// <summary>
        /// 枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<LineD> GetEnumerator()
        {
            Int32 i = 0;
            while (this.Points != null && i < (this.Points.Count - 1))
            {
                yield return new LineD(this.Points[i], this.Points[i + 1]);
                ++i;
            }

        }
        #endregion

        #region IEnumerable 成员
        /// <summary>
        /// 枚举器
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
