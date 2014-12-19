using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Howell.Drawing.D2
{
    /// <summary>
    /// 点集合
    /// </summary>
    public class PointSetD : IEquatable<PointSetD>, IEnumerable<PointD>
    {
        
        /// <summary>
        /// 正则表达式
        /// </summary>
        internal const String RegularExpressionString = @"^\{(Point=(?<Point>\{X=(?<X>[+-]?(?:\d+)(?:\.\d+)?),Y=(?<Y>[+-]?(?:\d+)(?:\.\d+)?)\}),?)+\}$";
        /// <summary>
        /// 空对象
        /// </summary>
        public static readonly PointSetD Empty = new PointSetD(null);


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="points">点集</param>        
        public PointSetD(IList<PointD> points)
        {
            m_Points = points;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="points">点集</param>        
        public PointSetD(PointD[] points)
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
        /// 点集
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
        /// 解析点集的字符串形式
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>如果解析成功，返回PolylineD类型，否则将抛出异常</returns>
        /// <exception cref="System.FormatException">非法的字符串格式。</exception>
        public static PointSetD Parse(String value)
        {            
            if (value == "") return new PointSetD(null);
            Regex reg = new Regex(RegularExpressionString, RegexOptions.Compiled);
            if (reg.IsMatch(value) == false) throw new FormatException(String.Format("{0}'s string format is illegal. {1}", typeof(PointSetD).FullName, value));
            MatchCollection collection = reg.Matches(value);
            CaptureCollection vertexs = collection[0].Groups["Point"].Captures;            
            List<PointD> points = new List<PointD>();
            for (int i = 0; i < vertexs.Count; ++i)
            {
                points.Add(PointD.Parse(vertexs[i].Value));
            }
            return new PointSetD(points.ToArray());
        }
        /// <summary>
        /// 获取点集的字符串型式
        /// </summary>
        /// <returns>返回点集的字符串型式。</returns>
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
            return (obj is PointSetD) ? (this == (PointSetD)obj) : false;
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
        /// <param name="p1">PointSetD object 1.</param>
        /// <param name="p2">PointSetD object 2.</param>
        /// <returns>true/false</returns>
        public static bool operator ==(PointSetD p1, PointSetD p2)
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
        /// <param name="p1">PointSetD object 1.</param>
        /// <param name="p2">PointSetD object 2.</param>
        /// <returns>true/false</returns>
        public static bool operator !=(PointSetD p1, PointSetD p2)
        {
            return !(p1 == p2);
        }


        /// <summary>
        /// 显式转换PointSetD To PointSetI
        /// </summary>
        /// <param name="pointSet">点集</param>
        /// <returns></returns>
        public static explicit operator PointSetI(PointSetD pointSet)
        {
            PointSetI result = PointSetI.Empty;
            if (pointSet.IsEmpty) return result;
            result.Points = new List<PointI>();
            for (Int32 i = 0; i < pointSet.Points.Count; ++i)
            {
                result.Points.Add((PointI)pointSet.Points[i]);
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
                return (this == PointSetD.Empty);
            }
        }
        #region IEquatable<PolylineD> 成员
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(PointSetD other)
        {
            return (this == other);
        }

        #endregion

        #region IEnumerable<PointD> 成员
        /// <summary>
        /// 枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<PointD> GetEnumerator()
        {
            Int32 i = 0;
            while (this.Points != null && i < (this.Points.Count - 1))
            {
                yield return new PointD(this.Points[i].X, this.Points[i].Y);
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
