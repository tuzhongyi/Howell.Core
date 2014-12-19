using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Howell.Drawing.D2
{
    /// <summary>
    /// 线2D
    /// </summary>
    public struct LineI : IEquatable<LineI>
    {
        /// <summary>
        /// 正则表达式
        /// </summary>
        internal const String RegularExpressionString = @"^\{Starting=(?<Starting>[0-9XY,.{}=]*),End=(?<End>[0-9XY,.{}=]*)\}$";
        /// <summary>
        /// 空对象
        /// </summary>
        public static readonly LineI Empty = new LineI(0,0,0,0);
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x1">起点的X轴坐标</param>
        /// <param name="y1">起点的Y轴坐标</param>
        /// <param name="x2">终点的X轴坐标</param>
        /// <param name="y2">终点的X轴坐标</param>
        public LineI(Int32 x1, Int32 y1, Int32 x2, Int32 y2)
            : this(new PointI(x1,y1),new PointI(x2,y2))
        {

        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="starting">开始</param>
        /// <param name="end">结束</param>
        public LineI(PointI starting, PointI end)
        {
            m_Starting = starting;
            m_End = end;
        }
        #region Property
        private PointI m_Starting;
        private PointI m_End;
        /// <summary>
        /// 是否为空
        /// </summary>
        public Boolean IsEmpty
        {
            get
            {
                return (this == LineI.Empty);
            }
        }
        /// <summary>
        /// 起点
        /// </summary>
        public PointI Starting
        {
            get
            {
                return m_Starting;
            }
            set
            {
                m_Starting = value;
            }
        }
        /// <summary>
        /// 终点
        /// </summary>
        public PointI End
        {
            get
            {

                return m_End;
            }
            set
            {
                m_End = value;
            }
        }
        #endregion


        #region operators

        /// <summary>
        /// 解析线段的字符串形式
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>如果解析成功，返回LineI类型，否则将抛出异常</returns>
        /// <exception cref="System.FormatException">非法的字符串格式。</exception>
        public static LineI Parse(String value)
        {
            Regex reg = new Regex(RegularExpressionString, RegexOptions.Compiled);
            if (reg.IsMatch(value) == false) throw new FormatException(String.Format("{0}'s string format is illegal. {1}", typeof(PointI).FullName, value));
            MatchCollection collection = reg.Matches(value);
            return new LineI(PointI.Parse(collection[0].Groups["Starting"].Value), PointI.Parse(collection[0].Groups["End"].Value));
        }
        /// <summary>
        /// 获取线段的字符串形式
        /// </summary>
        /// <returns>返回字符串形式</returns>
        public override string ToString()
        {
            return ("{Starting=" + this.Starting.ToString() + ",End=" + this.End.ToString() + "}");
        }
        /// <summary>
        /// Check if this instance of <see cref="LineI"/> equal to the specified one.
        /// </summary>
        /// <param name="obj">Another point to check equalty to.</param>
        /// <returns>Return <see langword="true"/> if objects are equal.</returns>
        public override bool Equals(object obj)
        {
            return (obj is LineI) ? (this == (LineI)obj) : false;
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
        /// equals operator - checks if two line is the same.
        /// </summary>
        /// <param name="l1">LineI object 1.</param>
        /// <param name="l2">LineI object 2.</param>
        /// <returns>true/false</returns>
        public static bool operator ==(LineI l1, LineI l2)
        {
            if (l1.Starting.Equals(l2.Starting) == false) return false;
            if (l1.End.Equals(l2.End) == false) return false;
            return true;
        }
        /// <summary>
        /// equals operator - checks if two line is not the same.
        /// </summary>
        /// <param name="l1">LineI object 1.</param>
        /// <param name="l2">LineI object 2.</param>
        /// <returns>true/false</returns>
        public static bool operator !=(LineI l1, LineI l2)
        {
            return !(l1 == l2);
        }
        /// <summary>
        /// 隐式转换
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static implicit operator LineD(LineI line)
        {
            return new LineD(line.Starting,line.End);
        }

        #endregion




        #region IEquatable<LineI> 成员
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(LineI other)
        {
            return (this == other);
        }

        #endregion
    }
}
