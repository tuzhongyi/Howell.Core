using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Howell.Drawing.D2
{
    /// <summary>
    /// 多边形
    /// </summary>
    public struct PolygonI : IEquatable<PolygonI>,IEnumerable<LineI>
    {
        /// <summary>
        /// 正则表达式
        /// </summary>
        internal const String RegularExpressionString = @"^\{(Vertex=(?<Vertex>\{X=(?<X>[+-]?\d+),Y=(?<Y>[+-]?\d+)\}),?)+\}$";
        /// <summary>
        /// 空对象
        /// </summary>
        public static readonly PolygonI Empty = new PolygonI(null);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="vertex">多边形的顶点</param>        
        public PolygonI(IList<PointI> vertex)
        {
            m_Vertex = vertex;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="vertex">多边形的顶点</param>        
        public PolygonI(PointI[] vertex)
        {
            m_Vertex = null;
            if (vertex == null) return;
            m_Vertex = new List<PointI>();
            for (Int32 i = 0; i < vertex.Length; ++i)
            {
                m_Vertex.Add(vertex[i]);
            }
        }
        #region property
        private IList<PointI> m_Vertex;
        /// <summary>
        /// 多边形顶点
        /// </summary>
        public IList<PointI> Vertex
        {
            get
            {
                return m_Vertex;
            }
            set
            {
                m_Vertex = value;
            }
        }
        /// <summary>
        /// 是否为空
        /// </summary>
        public Boolean IsEmpty
        {
            get
            {
                return (this == PolygonI.Empty);
            }
        }
        #endregion

        #region operator
        /// <summary>
        /// 解析多边形的字符串形式
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>如果解析成功，返回PolygonI类型，否则将抛出异常</returns>
        /// <exception cref="System.FormatException">非法的字符串格式。</exception>
        public static PolygonI Parse(String value)
        {
            if (value == "") return new PolygonI(null);
            Regex reg = new Regex(RegularExpressionString, RegexOptions.Compiled);
            if (reg.IsMatch(value) == false) throw new FormatException(String.Format("{0}'s string format is illegal. {1}", typeof(PolygonI).FullName, value));
            MatchCollection collection = reg.Matches(value);
            CaptureCollection vertexs = collection[0].Groups["Vertex"].Captures;
            List<PointI> points = new List<PointI>();
            for (int i = 0; i < vertexs.Count; ++i)
            {
                points.Add(PointI.Parse(vertexs[i].Value));
            }
            return new PolygonI(points.ToArray());
        }
        /// <summary>
        /// 获取多边形类型的字符串型式
        /// </summary>
        /// <returns>返回多边形类型的字符串型式。</returns>
        public override string ToString()
        {
            String result = "";
            if (m_Vertex != null)
            {
                result += "{";
                for (int i = 0; i < m_Vertex.Count; ++i)
                {
                    if (i > 0) result += ",";
                    result += String.Format("Vertex={0}", m_Vertex[i].ToString());
                }
                result += "}";
            }
            return result;
        }
        /// <summary>
        /// Check if this instance of <see cref="PolygonI"/> equal to the specified one.
        /// </summary>
        /// <param name="obj">Another point to check equalty to.</param>
        /// <returns>Return <see langword="true"/> if objects are equal.</returns>
        public override bool Equals(object obj)
        {
            return (obj is PolygonI) ? (this == (PolygonI)obj) : false;
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
        /// <param name="p1">Polygon object 1.</param>
        /// <param name="p2">Polygon object 2.</param>
        /// <returns>true/false</returns>
        public static bool operator ==(PolygonI p1, PolygonI p2)
        {
            if (p1.Vertex == null && p2.Vertex == null) return true;
            if (p1.Vertex == null || p2.Vertex == null) return false;
            if (p1.Vertex.Count != p2.Vertex.Count) return false;
            for (Int32 i = 0; i < p1.Vertex.Count; ++i)
            {
                if (p1.Vertex[i].Equals(p2.Vertex[i]) == false) return false;
            }
            return true;
        }

        /// <summary>
        /// equals operator - checks if two polygon is not the same.
        /// </summary>
        /// <param name="p1">Polygon2D object 1.</param>
        /// <param name="p2">Polygon2D object 2.</param>
        /// <returns>true/false</returns>
        public static bool operator !=(PolygonI p1, PolygonI p2)
        {
            return !(p1 == p2);
        }


        /// <summary>
        /// 显式转换PolygonD To PolygonI
        /// </summary>
        /// <param name="polygon">多边形</param>
        /// <returns></returns>
        public static implicit operator PolygonD(PolygonI polygon)
        {
            PolygonD result = PolygonD.Empty;
            if (polygon.IsEmpty) return result;
            result.Vertex = new List<PointD>();
            for (Int32 i = 0; i < polygon.Vertex.Count; ++i)
            {
                result.Vertex.Add(polygon.Vertex[i]);
            }
            return result;
        }
        #endregion

        #region IEquatable<PolygonI> 成员
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(PolygonI other)
        {
            return (this == other);
        }

        #endregion


        #region IEnumerable<LineI> 成员
        /// <summary>
        /// 枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<LineI> GetEnumerator()
        {
            Int32 i = 0;
            while (this.Vertex != null && i < (this.Vertex.Count))
            {
                yield return new LineI(this.Vertex[i], this.Vertex[(i + 1) % this.Vertex.Count]);
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
